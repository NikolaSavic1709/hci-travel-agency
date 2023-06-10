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

namespace travelAgency.Dialogs
{
    /// <summary>
    /// Interaction logic for EditPlaceDialog.xaml
    /// </summary>
    public partial class EditPlaceDialog : Window
    {
        Place _place;
        public TravelAgencyContext dbContext;
        public PlaceRepository placeRepository;
        public EditPlaceDialog(Place place)
        {
            InitializeComponent();
            dbContext = new TravelAgencyContext();
            placeRepository = new PlaceRepository(dbContext);

            _place = place;
            NameTxtBox.Text = place.Name;
            LocationTxtBox.Text = place.Location;
            DescriptionTxtBox.Text = place.Description;
        }

        public event EventHandler<DialogResultEventArgs> DialogResultEvent;
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResultEvent?.Invoke(this, new DialogResultEventArgs(false));
            Close();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {

            _place.Name = NameTxtBox.Text;
            _place.Location = LocationTxtBox.Text;
            _place.Description = DescriptionTxtBox.Text;
            placeRepository.Update(_place);

            DialogResultEvent?.Invoke(this, new DialogResultEventArgs(true));
            Close();
        }
    }
}
