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
using System.Windows.Shapes;
using travelAgency.components;
using travelAgency.controls;
using travelAgency.model;
using travelAgency.repository;

namespace travelAgency.view
{
    /// <summary>
    /// Interaction logic for AgentHome.xaml
    /// </summary>
    public partial class AgentHome : Window
    {
        public TravelAgencyContext dbContext;
        public TripRepository tripRepository;

        public string BingKey { get; set; }
        public AgentHome()
        {
            BingKey = "";
            InitializeComponent();

            HomeButton.IsClicked = "True";

            dbContext = new TravelAgencyContext();
            tripRepository = new TripRepository(dbContext);

            List<Trip> trips = tripRepository.GetAll();
            foreach(Trip t in trips)
            {
                TripCard tripCard = new TripCard
                {
                    Margin = new Thickness(10),
                    TripName = t.Name,
                    Route = t.Price.ToString(),
                    Description = t.Description
                };
                cards.Children.Add(tripCard);
            }

            TripCard tripCard1 = new TripCard
            {
                Margin = new Thickness(10),
                TripName = "Tura zapadna Srbija",
                Route = "Šabac - Bajina Bašta",
                Description = "Tura je veoma zaniljiva i duga jer Savic i drugari imaju sta da ponude i njihovom kraju"
            };

            TripCard tripCard2 = new TripCard
            {
                Margin = new Thickness(10),
                TripName = "Planinski maratoni",
                Route = "Raška - Pančićev vrh",
                Description = "Tura je veoma zaniljiva i duga jer Savic i drugari imaju sta da ponude i njihovom kraju"
            };

            TripCard tripCard3 = new TripCard
            {
                Margin = new Thickness(10),
                TripName = "Tura južna Srbija",
                Route = "Vranje - Đavolja Varoš",
                Description = "Ubedljiva najbolja tura u nasoj ponudi"
            };

            cards.Children.Add(tripCard1);
            cards.Children.Add(tripCard2);
            cards.Children.Add(tripCard3);
        }

        private void Search_OnKeyDown(object sender, KeyEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (e.Key == Key.Enter)
                SearchButton.Command.Execute(textBox.Text);
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

        private void NavbarButton_Click(object sender, RoutedEventArgs e)
        {
            NavbarButton button = (NavbarButton)sender;
            ClickNavbarButton(button);
        }
        private void ClickNavbarButton(NavbarButton clickedButton)
        {
            NavbarButton button;
            if (NavbarButtons != null)
            {
                foreach (var child in GetChildren(NavbarButtons))
                {
                    button = (NavbarButton)child;
                    if (button == clickedButton)
                        button.IsClicked = "True";
                    else
                        button.IsClicked = "False";
                }
            }
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
