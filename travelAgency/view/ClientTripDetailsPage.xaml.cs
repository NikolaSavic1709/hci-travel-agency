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
        Trip Trip { get; set; }
        TripDetailsViewModel? ViewModel { get; set; }

        public TripRepository tripRepository;
        private PlaceRepository placeRepository;

        public ClientTripDetailsPage(Trip trip, TripRepository tripRepository, PlaceRepository placeRepository)
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

        private void Purchase_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Reserve_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
