using DevExpress.Xpf.Core;
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
using travelAgency.Dialogs;
using travelAgency.model;
using travelAgency.repository;
using travelAgency.ViewModel;

namespace travelAgency.view
{
    /// <summary>
    /// Interaction logic for StayEatPage.xaml
    /// </summary>
    public partial class AdminStayEatDetailPage : Page
    {

        string ImageDirectory { get; set; }
        string[] ImageNames { get; set; }
        int CurrentImageIndex { get; set; }
        Place Place { get; set; }

        public TravelAgencyContext dbContext;
        public StayRepository stayRepository;
        public PlaceRepository placeRepository;
        public AdminStayEatDetailPage(Place place,bool isStay)
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
                } else
                {
                    AmenitiesSegment.Visibility = Visibility.Collapsed;
                }
                
                
            }

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

        private void geocodeProvider_LocationInformationReceived(object sender, LocationInformationReceivedEventArgs e)
        {

            GeocodeRequestResult result = e.Result;
            StringBuilder resultList = new StringBuilder("");
            resultList.Append(String.Format("Status: {0}\n", result.ResultCode));
            resultList.Append(String.Format("Fault reason: {0}\n", result.FaultReason));
            resultList.Append(String.Format("______________________________\n"));

            if (result.ResultCode != RequestResultCode.Success)
            {
                tbResults.Text = resultList.ToString();
                return;
            }

            int resCounter = 1;
            foreach (LocationInformation locations in result.Locations)
            {
                resultList.Append(String.Format("Request Result {0}:\n", resCounter));
                resultList.Append(String.Format("Display Name: {0}\n", locations.DisplayName));
                resultList.Append(String.Format("Entity Type: {0}\n", locations.EntityType));
                resultList.Append(String.Format("Address: {0}\n", locations.Address));
                resultList.Append(String.Format("Location: {0}\n", locations.Location));
                resultList.Append(String.Format("______________________________\n"));
                resCounter++;
            }

            tbResults.Text = resultList.ToString();
        }

        private MapPushpin mapItem;

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


        private void EditData_Click(object sender, RoutedEventArgs e)
        {
            EditPlaceDialog editPlaceDialog = new EditPlaceDialog(Place);

            editPlaceDialog.DialogResultEvent += EditPlace_DialogResultEvent;

            editPlaceDialog.ShowDialog();
        }

        private void EditPlace_DialogResultEvent(object sender, DialogResultEventArgs e)
        {
            bool result = e.Result;

            if (result)
            {
                int id = Place.Id;
                Place = placeRepository.GetById(id);
                RefreshPlaceData();
            }
        }

        private void RefreshPlaceData()
        {
            var viewModel = DataContext as StayEatViewModel;

            if (viewModel != null)
            {
                Place place = placeRepository.GetById(Place.Id);
                viewModel.Place = place;
            }

        }

        private void EditAmenities_Click(object sender, RoutedEventArgs e)
        {
            AmenitiesDialog amenitiesDialog = new AmenitiesDialog((Stay)Place);

            amenitiesDialog.DialogResultEvent += Amenities_DialogResultEvent;

            amenitiesDialog.ShowDialog();
        }

        private void Amenities_DialogResultEvent(object sender, DialogResultEventArgs e)
        {
            bool result = e.Result;

            if (result)
            {
                int id = Place.Id;
                Place = stayRepository.GetById(id);
                RefreshAmenities();
            }
        }
    }
}
