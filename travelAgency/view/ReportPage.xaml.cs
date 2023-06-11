using DevExpress.Xpf.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using travelAgency.components;
using travelAgency.controls;
using travelAgency.Dialogs;
using travelAgency.model;
using travelAgency.repository;

namespace travelAgency.view
{
    /// <summary>
    /// Interaction logic for ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        public TripRepository tripRepository;
        public ArrangementRepository arrangementRepository;
        public (int month, int year) lastMonthFilter=(0,0);
        public string BingKey { get; set; }
        public ReportPage(TripRepository tripRepository, ArrangementRepository arrangementRepository)
        {
            this.tripRepository = tripRepository;
            this.arrangementRepository = arrangementRepository;
            BingKey = "";
            InitializeComponent();
            Grid calendarGrid = Calendar;

            if (calendarGrid.Children.Count >= 6 && calendarGrid.Children[5] is CalendarLabel label)
            {
                MouseButtonEventArgs args = new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left);
                args.RoutedEvent = Mouse.MouseDownEvent;
                label.RaiseEvent(args);
            }

        }
        private void DecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            int number = int.Parse(YearTb.Text);
            if (number > 1) YearTb.Text = (number - 1).ToString();
        }

        private void IncreaseButton_Click(object sender, RoutedEventArgs e)
        {
            int number = int.Parse(YearTb.Text);
            if (number < 2023) YearTb.Text = (number + 1).ToString();
        }

        private void NumericTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {

            foreach (var c in e.Text)
            {
                if (!char.IsDigit(c))
                {
                    e.Handled = true;
                    return;
                }
            }
            int number = int.Parse(((TextBox)sender).Text + e.Text);
            if (number > 2023)
            {
                e.Handled = true; return;
            }
        }

        private void SelectMonth_Click(object sender, MouseButtonEventArgs e)
        {
            CalendarLabel label = (CalendarLabel)sender;
            int month = MonthNameToNumber(label.Content.ToString());
            int year = Convert.ToInt32(YearTb.Text);
            if((month,year)!=lastMonthFilter)
            {
                lastMonthFilter = (month, year);
                SelectMonthLabels(label);
                DoFilterPerMonth(month, year);
            }
        }

        private void DoFilterPerMonth(int month, int year)
        {
            cards.Children.Clear();
            List<Arrangement> arrangements = arrangementRepository.GetAll().Where(a => a.DateTime.Month == month && a.DateTime.Year == year).ToList();
            foreach (var arrangement in arrangements)
            {
                CreateCard(arrangement);
            }
        }
        private void CreateCard(Arrangement arrangement)
        {
            ReportCard reportCard = new ReportCard
            {
                Margin = new Thickness(10),
                Arrangement = arrangement

            };

            cards.Children.Add(reportCard);
        }
        private int MonthNameToNumber(String month)
        {
            switch (month)
            {
                case "JAN":
                    return 1;
                case "FEB":
                    return 2;
                case "MAR":
                    return 3;
                case "APR":
                    return 4;
                case "MAY":
                    return 5;
                case "JUN":
                    return 6;
                case "JUL":
                    return 7;
                case "AUG":
                    return 8;
                case "SEP":
                    return 9;
                case "OCT":
                    return 10;
                case "NOV":
                    return 11;
                case "DEC":
                    return 12;
                default:
                    return 6;
            }
        }

        private void SelectMonthLabels(CalendarLabel selectedLabel)
        {
            CalendarLabel label;
            if (Calendar != null)
            {
                foreach (var child in GetChildren(Calendar))
                {
                    label = (CalendarLabel)child;
                    if (label == selectedLabel)
                        label.IsSelected = "True";
                    else
                        label.IsSelected = "False";
                }
            }
        }

        private IEnumerable<DependencyObject> GetChildren(DependencyObject parent)
        {
            if (parent == null)
                yield break;

            int childCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                yield return child;

            }
        }
        private void Search_OnKeyDown(object sender, KeyEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (e.Key == Key.Enter)
                SearchButton.Command.Execute(textBox.Text);
        }
        private void geocodeProvider_LocationInformationReceived(object sender, LocationInformationReceivedEventArgs e)
        {

            GeocodeRequestResult result = e.Result;
            StringBuilder resultList = new StringBuilder("");
            resultList.Append(String.Format("Status: {0}\n", result.ResultCode));
            resultList.Append(String.Format("Fault reason: {0}\n", result.FaultReason));
            resultList.Append(String.Format("______________________________\n"));

            if (result.ResultCode != RequestResultCode.Success)
            {
                tbResults.Text = resultList.ToString();
                return;
            }

            int resCounter = 1;
            foreach (LocationInformation locations in result.Locations)
            {
                resultList.Append(String.Format("Request Result {0}:\n", resCounter));
                resultList.Append(String.Format("Display Name: {0}\n", locations.DisplayName));
                resultList.Append(String.Format("Entity Type: {0}\n", locations.EntityType));
                resultList.Append(String.Format("Address: {0}\n", locations.Address));
                resultList.Append(String.Format("Location: {0}\n", locations.Location));
                resultList.Append(String.Format("______________________________\n"));
                resCounter++;
            }

            tbResults.Text = resultList.ToString();
        }

        private MapPushpin mapItem;

        private void map_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {


            var hitInfo = map.CalcHitInfo(e.GetPosition(map));
            if (hitInfo.InMapPushpin)
            {
                map.EnableScrolling = false;
                mapItem = hitInfo.HitObjects[0] as MapPushpin;

            }

        }

        private void map_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (mapItem != null)
            {
                var point = map.ScreenPointToCoordPoint(e.GetPosition(map));
                mapItem.Location = point;
                map.EnableScrolling = true;
                mapItem = null;
            }
        }

        private void map_MouseMove(object sender, MouseEventArgs e)
        {
            if (mapItem != null)
            {
                var point = map.ScreenPointToCoordPoint(e.GetPosition(map));
                mapItem.Location = point;

            }
        }

    }
}