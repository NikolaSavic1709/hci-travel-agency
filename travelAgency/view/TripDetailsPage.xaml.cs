using System.Windows;
using System.Windows.Controls;
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
        Trip Trip { get; set; }
        TripDetailsViewModel? ViewModel { get; set; }

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
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            CreateTripScheduleDialog dialog = new CreateTripScheduleDialog(Trip, tripRepository, placeRepository);
            dialog.Show();
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
            TourEdit dialog = new TourEdit(Trip,tripRepository);
            dialog.Show();
        }
    }
}