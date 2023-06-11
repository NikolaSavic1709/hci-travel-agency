using DevExpress.Xpf.Map;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using travelAgency.components;
using travelAgency.Dialogs;
using travelAgency.model;
using travelAgency.repository;

namespace travelAgency.view
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {

        public TripRepository tripRepository;
        public PlaceRepository placeRepository;
        public string BingKey { get; set; }
        public HomePage(TripRepository tripRepository, PlaceRepository placeRepository)
        {
            BingKey = "";
            InitializeComponent();
            this.tripRepository = tripRepository;
            this.placeRepository = placeRepository;

            List<Trip> trips = tripRepository.GetAll();


            foreach (Trip t in trips)
            {
                CreateCard(t);
            }
        }
        private void CreateCard(Trip trip)
        {
            TripCard tripCard = new TripCard
            {
                Margin = new Thickness(10),
                Trip = trip

            };
            tripCard.ToTripClicked += TripCard_ToTrip;
            tripCard.TripDelete += TripCard_Remove;
            tripCard.MouseDown += TripCard_MouseDown;
            cards.Children.Add(tripCard);
        }
        private void TripCard_ToTrip(object sender, ToTripEventArgs e)
        {
            Trip trip = e.Trip;

            NavigationService?.Navigate(new TripDetailsPage(trip, tripRepository, placeRepository));
        }
        private void TripCard_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Trip trip = (sender as TripCard).Trip;
            List<RouteWaypoint> waypoints = new List<RouteWaypoint>();
            foreach (var schedule in trip.Schedules)
            {
                waypoints.Add(new RouteWaypoint(schedule.Place.Name, new GeoPoint(schedule.Place.lat, schedule.Place.lng)));
            }
            routeProvider.CalculateRoute(waypoints);
        }
        private void TripCard_NewTrip(object sender, ToTripEventArgs e)
        {
            CreateCard(e.Trip);
        }
        private void TripCard_Remove(object sender, ToTripEventArgs e)
        {
            Trip trip = e.Trip;
            TripCard card = (TripCard)sender;
            cards.Children.Remove(card);
            tripRepository.Delete(trip);

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

        private void CreateTrip_Click(object sender, RoutedEventArgs e)
        {
            CreateTripDialog window = new CreateTripDialog(tripRepository, placeRepository);
            window.ShowDialog();
        }

        private void routeProvider_LayerItemsGenerating(object sender, LayerItemsGeneratingEventArgs args)
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
    }
}