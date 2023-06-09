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
        public TripDetailsPage(Trip trip)
        {
            InitializeComponent();
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
            CreateTripScheduleDialog dialog = new CreateTripScheduleDialog(Trip);
            dialog.Show();
        }

        private void RemovePlace_Click(object sender, RoutedEventArgs e)
        {
            Button removeButton = (Button)sender;
            TripSchedule tripSchedule = (TripSchedule)removeButton.DataContext;
            if(ViewModel!=null)
                ViewModel.Trip.Schedules.Remove(tripSchedule);
        }

        private void EditPlace_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
