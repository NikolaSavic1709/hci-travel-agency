using DevExpress.Xpf.Map;
using DevExpress.XtraPrinting.Native;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
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
        private AttractionRepository attractionRepository;
        private StayRepository stayRepository;
        private RestaurantRepository restaurantRepository;
        public TravelAgencyContext dbContext;
        private MapPushpin mapItem;
        private double lat;
        private double lng;

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
            String selectedOption = ((ComboBoxItem)OutlinedComboBox.SelectedItem).Content.ToString();
            if (selectedOption == "Attraction")
            {
                Attraction place = new Attraction();
                place.Name = NameTxtBox.Text;
                place.Description = DescriptionTxtBox.Text;
                place.Location = LocationTxtBox.Text;
                place.lat = lat;
                place.lng = lng;
                attractionRepository.Add(place);
                NewAttraction?.Invoke(this, new ToAttractionEventArgs((Attraction)place));
            }
            else if (selectedOption == "Accomodation")
            {
                Stay place = new Stay();
                place.Name = NameTxtBox.Text;
                place.Description = DescriptionTxtBox.Text;
                place.Location = LocationTxtBox.Text;
                stayRepository.Add(place);
                place.lat = lat;
                place.lng = lng;


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
                restaurantRepository.Add(place);
                place.lat = lat;
                place.lng = lng;
                NewStayEat?.Invoke(this, new ToStayEatEventArgs(place));
            }

            Close();
        }

        private void GeocodeProvider_LocationInformationReceived(object sender, LocationInformationReceivedEventArgs e)
        {
            searchLayer.ClearResults();
            GeocodeRequestResult result = e.Result;
            StringBuilder resultList = new StringBuilder("");
            resultList.Append(String.Format("Status: {0}\n", result.ResultCode));
            resultList.Append(String.Format("Fault reason: {0}\n", result.FaultReason));
            resultList.Append(String.Format("______________________________\n"));

            if (result.ResultCode != RequestResultCode.Success)
            {
                return;
            }

            foreach (LocationInformation locations in result.Locations)
            {
                lat = locations.Location.Latitude;
                lng = locations.Location.Longitude;
            }
        }

        private void map_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var hitInfo = map.CalcHitInfo(e.GetPosition(map));
            if (hitInfo.InMapPushpin)
            {
                map.EnableScrolling = false;
                mapItem = hitInfo.HitObjects[0] as MapPushpin;
            }
        }

        private void map_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (mapItem != null)
            {
                var point = map.ScreenPointToCoordPoint(e.GetPosition(map));
                mapItem.Location = point;
                map.EnableScrolling = true;
                mapItem = null;
                lng = point.GetX();
                lat = point.GetY();
            }
        }

        private void map_MouseMove(object sender, MouseEventArgs e)
        {
            if (mapItem != null)
            {
                var point = map.ScreenPointToCoordPoint(e.GetPosition(map));
                mapItem.Location = point;
            }
        }

        private void BingSearchDataProvider_SearchCompleted(object sender, BingSearchCompletedEventArgs e)
        {
            coderLayer.ClearResults();
            SearchRequestResult result = e.RequestResult;

            if (result.ResultCode != RequestResultCode.Success)
            {
                return;
            }

            foreach (LocationInformation locations in result.SearchResults)
            {
                lat = locations.Location.Latitude;
                lng = locations.Location.Longitude;
            }
        }
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