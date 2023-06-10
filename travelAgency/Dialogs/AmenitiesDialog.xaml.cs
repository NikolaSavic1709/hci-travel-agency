using DevExpress.Internal.WinApi.Windows.UI.Notifications;
using Microsoft.EntityFrameworkCore;
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
using travelAgency.model;
using travelAgency.repository;
using travelAgency.ViewModel;

namespace travelAgency.Dialogs
{
    /// <summary>
    /// Interaction logic for AmenitiesDialog.xaml
    /// </summary>
    public partial class AmenitiesDialog : Window
    {
        private AmenitiesDialogViewModel amenitiesDialogViewModel;
        private Stay _stay;
        public TravelAgencyContext dbContext;
        public StayRepository stayRepository;
        public AmenitiesDialog(Stay stay)
        {
            InitializeComponent();
            dbContext = new TravelAgencyContext();
            stayRepository = new StayRepository(dbContext);

            _stay = stay;
            amenitiesDialogViewModel = new AmenitiesDialogViewModel(stay);
            DataContext = amenitiesDialogViewModel;
        }

        private void RemoveAllBtn_Click(object sender, RoutedEventArgs e)
        {
            amenitiesDialogViewModel.RemoveAll();
        }

        private void AddAllBtn_Click(object sender, RoutedEventArgs e)
        {
            amenitiesDialogViewModel.AddAll();
        }


        public event EventHandler<DialogResultEventArgs> DialogResultEvent;

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResultEvent?.Invoke(this, new DialogResultEventArgs(false));
            Close();
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as AmenitiesDialogViewModel;
            IEnumerable<IconItemViewModel> todoItemViewModels = viewModel.ActiveIconItemListingViewModel.TodoItemViewModels;

            List<Amenity> amenities = new List<Amenity>();
            
            foreach (var itemViewModel in todoItemViewModels)
            {
                Amenity amenity = new Amenity();
                amenity.amenity = (AmenityEnum)itemViewModel.KindValue;
                amenities.Add(amenity);
            }

            _stay.StayAmenities = amenities;
            stayRepository.Update(_stay);

            DialogResultEvent?.Invoke(this, new DialogResultEventArgs(true));
            Close();
        }
    }
}
