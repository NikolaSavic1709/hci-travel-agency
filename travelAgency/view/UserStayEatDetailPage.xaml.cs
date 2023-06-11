using DevExpress.Xpf.Map;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;
using travelAgency.components;
using travelAgency.Dialogs;
using travelAgency.model;
using travelAgency.repository;
using travelAgency.ViewModel;

namespace travelAgency.view
{
    /// <summary>
    /// Interaction logic for UserStayEatDetailPage.xaml
    /// </summary>
    public partial class UserStayEatDetailPage : Page
    {
        private string ImageDirectory { get; set; }
        private string[] ImageNames { get; set; }
        private int CurrentImageIndex { get; set; }
        private Place Place { get; set; }

        public TravelAgencyContext dbContext;
        public StayRepository stayRepository;
        public PlaceRepository placeRepository;

        public UserStayEatDetailPage(Place place, bool isStay)
        {
            InitializeComponent();
            Place = place;
            DataContext = new StayEatViewModel(place);

            dbContext = new TravelAgencyContext();
            stayRepository = new StayRepository(dbContext);
            placeRepository = new PlaceRepository(dbContext);

            // TODO: add directory for every place
            ImageDirectory = "C:\\semestar6\\HCI\\vezbe\\projekat\\hci-travel-agency\\travelAgency\\Resources\\Images";
            ImageNames = Directory.GetFiles(ImageDirectory);
            CurrentImageIndex = 0;
            var viewModel = DataContext as StayEatViewModel;
            if (viewModel != null)
            {
                viewModel.FrontImageSource = ImageNames[CurrentImageIndex];
                viewModel.BackImageSource = ImageNames[CurrentImageIndex];

                if (isStay)
                {
                    AmenitiesSegment.Visibility = Visibility.Visible;
                    RefreshAmenities();
                }
                else
                {
                    AmenitiesSegment.Visibility = Visibility.Collapsed;
                }
            }
            DrawPin();
        }

        private void RefreshAmenities()
        {
            var viewModel = DataContext as StayEatViewModel;

            if (viewModel != null)
            {
                Stay stay = stayRepository.GetById(Place.Id);
                viewModel.ClearTodoItemViewModels();

                for (int i = 0; i < stay.StayAmenities.Count; i++)
                {
                    int amenityIdx = (int)stay.StayAmenities[i].amenity;
                    viewModel.AddTodoItem(new IconItemViewModel(amenityIdx));
                }
            }
        }

        private void SlideLeft(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as StayEatViewModel;
            if (viewModel != null)
            {
                CurrentImageIndex = (CurrentImageIndex - 1 + ImageNames.Length) % ImageNames.Length;
                viewModel.BackImageSource = ImageNames[CurrentImageIndex];
                DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.1) };
                timer.Tick += (s, args) =>
                {
                    ImageFlipper.IsFlipped = !ImageFlipper.IsFlipped;
                    viewModel.FrontImageSource = viewModel.BackImageSource;
                    timer.Stop();
                };
                timer.Start();
            }
        }

        private void SlideRight(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as StayEatViewModel;
            if (viewModel != null)
            {
                CurrentImageIndex = (CurrentImageIndex + 1) % ImageNames.Length;
                viewModel.BackImageSource = ImageNames[CurrentImageIndex];
                DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.1) };
                timer.Tick += (s, args) =>
                {
                    ImageFlipper.IsFlipped = !ImageFlipper.IsFlipped;
                    viewModel.FrontImageSource = viewModel.BackImageSource;
                    timer.Stop();
                };
                timer.Start();
            }
        }

        private MapPushpin pin;

        public void DrawPin()
        {
            if (pin != null)
                mapItems.Items.Remove(pin);
            pin = new MapPushpin();
            pin.Location = new GeoPoint(Place.lat, Place.lng);
            pin.Information = Place.Name;
            pin.LocationChangedAnimation = new PushpinLocationAnimation();
            pin.CanMove = false;
            mapItems.Items.Add(pin);
        }
    }
}