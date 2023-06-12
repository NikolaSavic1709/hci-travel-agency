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
using travelAgency.Dialogs;
using travelAgency.model;
using travelAgency.repository;
using travelAgency.ViewModel;

namespace travelAgency.view
{
    /// <summary>
    /// Interaction logic for ClientTripDetailsPage.xaml
    /// </summary>
    public partial class ClientTripDetailsPage : Page
    {
        private Trip Trip { get; set; }
        private TripDetailsViewModel? ViewModel { get; set; }

        public TripRepository tripRepository;
        private PlaceRepository placeRepository;

        public ArrangementRepository arrangementRepository;
        private TravelAgencyContext dbContext;
        private User loggedUser;

        public ClientTripDetailsPage(Trip trip, TravelAgencyContext dbContext, User loggedUser)
        {
            InitializeComponent();
            this.dbContext = dbContext;
            this.tripRepository = new TripRepository(dbContext);
            this.placeRepository = new PlaceRepository(dbContext);
            this.arrangementRepository = new ArrangementRepository(dbContext);
            this.loggedUser = loggedUser;
            Trip = trip;
            DataContext = new TripDetailsViewModel();

            ViewModel = DataContext as TripDetailsViewModel;
            if (ViewModel != null)
            {
                ViewModel.Trip = trip;
            }
            DrawRoute();
            Loaded += Page_Loaded;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            FocusManager.SetFocusedElement(this, PurchaseBtn);
            Keyboard.Focus(this);
        }

        private void Purchase_Click(object sender, RoutedEventArgs e)
        {
            Arrangement arrangement = new Arrangement();
            arrangement.Trip = Trip;
            arrangement.User = loggedUser;
            arrangement.IsReservation = false;
            (new BuyReservation(arrangement, dbContext)).ShowDialog();
        }

        private void Reserve_Click(object sender, RoutedEventArgs e)
        {
            Arrangement arrangement = new Arrangement();
            arrangement.Trip = Trip;
            arrangement.User = loggedUser;
            arrangement.IsReservation = true;
            (new BuyReservation(arrangement, dbContext)).ShowDialog();
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

        private void Purchase_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Arrangement arrangement = new Arrangement();
            arrangement.Trip = Trip;
            arrangement.User = loggedUser;
            arrangement.IsReservation = false;
            (new BuyReservation(arrangement, dbContext)).ShowDialog();
        }

        private void Reserve_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Arrangement arrangement = new Arrangement();
            arrangement.Trip = Trip;
            arrangement.User = loggedUser;
            arrangement.IsReservation = true;
            (new BuyReservation(arrangement, dbContext)).ShowDialog();
        }
    }
}