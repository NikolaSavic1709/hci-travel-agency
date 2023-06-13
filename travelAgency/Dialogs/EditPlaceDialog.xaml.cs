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
        public string Name1 { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public EditPlaceDialog(Place place)
        {
            InitializeComponent();
            dbContext = new TravelAgencyContext();
            placeRepository = new PlaceRepository(dbContext);
            DataContext = this;
            _place = place;
            Name1 = place.Name;
            Location = place.Location;
            Description = place.Description;
            NameTxtBox.Focus();
        }

        public event EventHandler<DialogResultEventArgs> DialogResultEvent;
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResultEvent?.Invoke(this, new DialogResultEventArgs(false));
            Close();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        public void Update()
        {
            _place.Name = NameTxtBox.Text;
            _place.Location = LocationTxtBox.Text;
            _place.Description = DescriptionTxtBox.Text;
            placeRepository.Update(_place);

            DialogResultEvent?.Invoke(this, new DialogResultEventArgs(true));
            Close();
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Update();
        }

        private void Quit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DialogResultEvent?.Invoke(this, new DialogResultEventArgs(false));
            Close();
        }
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            BindingExpression bindingExpr = textBox.GetBindingExpression(TextBox.TextProperty);

            // Manually trigger the validation
            bindingExpr.UpdateSource();
        }
    }
}
