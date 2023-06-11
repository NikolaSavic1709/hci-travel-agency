using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using travelAgency.model;
using travelAgency.repository;
using travelAgency.ViewModel;

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

            DataContext = this;
            SetAmenities();
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
                place.lat = 46;
                place.lng = 20;

                IEnumerable<IconItemViewModel> todoItemViewModels = ActiveIconItemListingViewModel.TodoItemViewModels;

                List<Amenity> amenities = new List<Amenity>();

                foreach (var itemViewModel in todoItemViewModels)
                {
                    Amenity amenity = new Amenity();
                    amenity.amenity = (AmenityEnum)itemViewModel.KindValue;
                    amenities.Add(amenity);
                }

                place.StayAmenities = amenities;

                stayRepository.Add(place);
                NewStayEat?.Invoke(this, new ToStayEatEventArgs(place));
            }
            else if (selectedOption == "Restaurant")
            {
                Restaurant place = new Restaurant();
                place.Name = NameTxtBox.Text;
                place.Description = DescriptionTxtBox.Text;
                place.Location = LocationTxtBox.Text;
                place.lat = 47;
                place.lng = 20;
                restaurantRepository.Add(place);
                NewStayEat?.Invoke(this, new ToStayEatEventArgs(place));
            }

            Close();
        }


        private void RemoveAllBtn_Click(object sender, RoutedEventArgs e)
        {
            RemoveAll();
        }

        private void AddAllBtn_Click(object sender, RoutedEventArgs e)
        {
            AddAll();
        }

        public IconItemListingViewModel ActiveIconItemListingViewModel { get; set; }
        public IconItemListingViewModel RemainingIconItemListingViewModel { get; set; }

        public void SetAmenities()
        {
            IconItemListingViewModel activeIconItemListingViewModel = new IconItemListingViewModel();

            ActiveIconItemListingViewModel = activeIconItemListingViewModel;

            IconItemListingViewModel remainingIconItemListingViewModel = new IconItemListingViewModel();
            for (int i = 0; i < 10; i++)
            {               
                remainingIconItemListingViewModel.AddTodoItem(new IconItemViewModel(i));
            }

            RemainingIconItemListingViewModel = remainingIconItemListingViewModel;
        }
        public void RemoveAll()
        {
            ObservableCollection<IconItemViewModel> movedIconItemViewModels = ActiveIconItemListingViewModel.RemoveAllTodoItems();
            foreach (IconItemViewModel item in movedIconItemViewModels)
            {
                RemainingIconItemListingViewModel.AddTodoItem(item);
            }
        }

        public void AddAll()
        {
            ObservableCollection<IconItemViewModel> movedIconItemViewModels = RemainingIconItemListingViewModel.RemoveAllTodoItems();
            foreach (IconItemViewModel item in movedIconItemViewModels)
            {
                ActiveIconItemListingViewModel.AddTodoItem(item);
            }
        }
    }









    public class VisibilityToGridHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibility = (Visibility)value;
            return visibility == Visibility.Visible ? new GridLength(350) : new GridLength(0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}