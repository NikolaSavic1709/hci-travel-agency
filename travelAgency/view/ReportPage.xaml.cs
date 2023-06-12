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
using travelAgency.Commands;
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
        public (int month, int year) lastMonthFilter = (0, 0);
        public string BingKey { get; set; }

        public List<ReportCard> reportCards;
        public List<ReportCard> searchReportCards;
        public List<ReportCard> filteredReportCards;
        public ICommand SearchCommand { get; }

        public ReportPage(TripRepository tripRepository, ArrangementRepository arrangementRepository)
        {
            this.tripRepository = tripRepository;
            this.arrangementRepository = arrangementRepository;
            BingKey = "";
            SearchCommand = new CommandImplementationcs(Search);
            reportCards = new List<ReportCard>();
            filteredReportCards = new List<ReportCard>();
            InitializeComponent();
            DataContext = this;

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
            if ((month, year) != lastMonthFilter)
            {
                lastMonthFilter = (month, year);
                SelectMonthLabels(label);
                DoFilterPerMonth(month, year);
            }
        }

        private void DoFilterPerMonth(int month, int year)
        {
            reportCards.Clear();
            List<Arrangement> arrangements = arrangementRepository.GetAll().Where(a => a.DateTime.Month == month && a.DateTime.Year == year).ToList();
            foreach (var arrangement in arrangements)
            {
                CreateCard(arrangement);
            }
            RefreshCards(false);
        }

        private void CreateCard(Arrangement arrangement)
        {
            ReportCard reportCard = new ReportCard
            {
                Margin = new Thickness(10),
                Arrangement = arrangement
            };

            reportCards.Add(reportCard);
            filteredReportCards.Add(reportCard);
        }

        public void RefreshCards(bool isFilter)
        {
            cards.Children.Clear();
            if (isFilter)
            {
                foreach (ReportCard a in searchReportCards)
                {
                    cards.Children.Add(a);
                }
            }
            else
            {
                foreach (ReportCard a in filteredReportCards)
                {
                    cards.Children.Add(a);
                }
            }
        }

        private void ReportCard_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DrawRoute((sender as ReportCard).Arrangement.Trip);
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

        private async void Search(object? obj)
        {
            var text = obj as string;
            if (string.IsNullOrWhiteSpace(text))
            {
                searchReportCards = filteredReportCards;
            }
            else
            {
                searchReportCards = await Task.Run(() => filteredReportCards
                    .Where(x => x.Trip.Name.IndexOf(text, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    .ToList());
            }
            RefreshCards(true);
        }

        public void routeProvider_LayerItemsGenerating(object sender, LayerItemsGeneratingEventArgs args)
        {
            char letter = 'A';
            foreach (MapItem item in args.Items)
            {
                MapPushpin pushpin = item as MapPushpin;
                if (pushpin != null)
                    pushpin.Text = letter++.ToString();
                MapPolyline line = item as MapPolyline;
                if (line != null)
                {
                    var converter = new System.Windows.Media.BrushConverter();
                    var brush = (Brush)converter.ConvertFromString("#009882");
                    line.Fill = brush;
                    line.Stroke = brush;
                }
            }

            map.ZoomToFit(args.Items);
        }

        public void DrawRoute(Trip trip)
        {
            List<RouteWaypoint> waypoints = new List<RouteWaypoint>();
            foreach (var schedule in trip.Schedules)
            {
                waypoints.Add(new RouteWaypoint(schedule.Place.Name, new GeoPoint(schedule.Place.lat, schedule.Place.lng)));
            }
            routeProvider.CalculateRoute(waypoints);
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            SearchBox.Text = "";
            FilterReportDialog filterReportDialog = new FilterReportDialog(reportCards);

            filterReportDialog.DialogResultEvent += Filter_DialogResultEvent;

            filterReportDialog.ShowDialog();
        }

        private void Filter_DialogResultEvent(object sender, ReportCardEventArgs e)
        {
            List<ReportCard> result = e.ReportCards;

            if (result != null)
            {
                filteredReportCards = result;
            }
            RefreshCards(false);
        }
    }
}