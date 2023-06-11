using DevExpress.Xpf.Map;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using travelAgency.components;
using travelAgency.Dialogs;
using travelAgency.model;
using travelAgency.repository;
using travelAgency.ViewModel;

namespace travelAgency.view
{
    /// <summary>
    /// Interaction logic for TripDetailsPage.xaml
    /// </summary>
    public partial class TripDetailsPage : Page
    {
        private Trip Trip { get; set; }
        private TripDetailsViewModel? ViewModel { get; set; }

        public TripRepository tripRepository;
        private PlaceRepository placeRepository;

        public TripDetailsPage(Trip trip, TripRepository tripRepository, PlaceRepository placeRepository)
        {
            InitializeComponent();
            this.tripRepository = tripRepository;
            this.placeRepository = placeRepository;

            Trip = trip;
            DataContext = new TripDetailsViewModel();

            ViewModel = DataContext as TripDetailsViewModel;
            if (ViewModel != null)
            {
                ViewModel.Trip = trip;
            }
            DrawRoute();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            CreateTripScheduleDialog dialog = new CreateTripScheduleDialog(Trip, tripRepository, placeRepository);
            dialog.ShowDialog();
            DrawRoute();
        }

        private void RemovePlace_Click(object sender, RoutedEventArgs e)
        {
            Button removeButton = (Button)sender;
            TripSchedule tripSchedule = (TripSchedule)removeButton.DataContext;
            if (ViewModel != null)
            {
                if (ViewModel.Trip.Schedules.Count > 1)
                {
                    ViewModel.Trip.Schedules.Remove(tripSchedule);
                    Trip.Schedules.Remove(tripSchedule);
                    this.tripRepository.Save();
                }
                else
                {
                    //snackbar
                }
            }
            DrawRoute();
        }

        private void EditPlace_Click(object sender, RoutedEventArgs e)
        {
            Button removeButton = (Button)sender;
            TripSchedule tripSchedule = (TripSchedule)removeButton.DataContext;
            Place place = tripSchedule.Place;

            PlaceEdit dialog = new PlaceEdit(place);
            dialog.Show();
        }

        private void EditTour_Click(object sender, RoutedEventArgs e)
        {
            TourEdit dialog = new TourEdit(Trip, tripRepository);
            dialog.Show();
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

        public void DrawRoute()
        {
            List<RouteWaypoint> waypoints = new List<RouteWaypoint>();
            foreach (var schedule in Trip.Schedules)
            {
                waypoints.Add(new RouteWaypoint(schedule.Place.Name, new GeoPoint(schedule.Place.lat, schedule.Place.lng)));
            }
            routeProvider.CalculateRoute(waypoints);
        }
    }
}