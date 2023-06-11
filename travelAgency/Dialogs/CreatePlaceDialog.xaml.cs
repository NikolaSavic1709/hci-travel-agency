using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using travelAgency.model;
using travelAgency.repository;

namespace travelAgency.Dialogs
{
    /// <summary>
    /// Interaction logic for CreatePlaceDialog.xaml
    /// </summary>
    public partial class CreatePlaceDialog : Window
    {
        AttractionRepository attractionRepository;
        StayRepository stayRepository;
        RestaurantRepository restaurantRepository;
        public TravelAgencyContext dbContext;
        public CreatePlaceDialog()
        {
            InitializeComponent();
            dbContext = new TravelAgencyContext();
            attractionRepository = new AttractionRepository(dbContext);
            stayRepository = new StayRepository(dbContext);
            restaurantRepository = new RestaurantRepository(dbContext);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OutlinedComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string selected = comboBox.SelectedItem.ToString();
            if (selected.Contains("Accomodation"))
                AmenitiesFragment.Visibility = Visibility.Visible;
            else
                AmenitiesFragment.Visibility = Visibility.Hidden;
        }
        public event EventHandler<ToAttractionEventArgs> NewAttraction;
        public event EventHandler<ToStayEatEventArgs> NewStayEat;
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            
            String selectedOption=((ComboBoxItem) OutlinedComboBox.SelectedItem).Content.ToString();
            if(selectedOption=="Attraction")
            {
                Attraction place = new Attraction();
                place.Name = NameTxtBox.Text;
                place.Description = DescriptionTxtBox.Text;
                place.Location = LocationTxtBox.Text;
                place.lat = 45;
                place.lng = 20;
                attractionRepository.Add(place);
                NewAttraction?.Invoke(this, new ToAttractionEventArgs((Attraction) place));
            }
            else if(selectedOption=="Accomodation")
            {
                Stay place = new Stay();
                place.Name = NameTxtBox.Text;
                place.Description = DescriptionTxtBox.Text;
                place.Location = LocationTxtBox.Text;
                stayRepository.Add(place);
                place.lat = 46;
                place.lng = 20;
                NewStayEat?.Invoke(this, new ToStayEatEventArgs(place));
            }
            else if (selectedOption == "Restaurant")
            {
                Restaurant place = new Restaurant();
                place.Name = NameTxtBox.Text;
                place.Description = DescriptionTxtBox.Text;
                place.Location = LocationTxtBox.Text;
                restaurantRepository.Add(place);
                place.lat = 47;
                place.lng = 20;
                NewStayEat?.Invoke(this, new ToStayEatEventArgs(place));
            }

            Close();
        }
    }

    public class VisibilityToGridHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibility = (Visibility)value;
            return visibility == Visibility.Visible ? new GridLength(200) : new GridLength(0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}