﻿using DevExpress.Xpf.Core;
using DevExpress.Xpf.Map;
using DevExpress.XtraRichEdit.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        private string ImageDirectory { get; set; }
        private string[] ImageNames { get; set; }
        private int CurrentImageIndex { get; set; }
        private Place Place { get; set; }

        private MapPushpin pin;

        public TravelAgencyContext dbContext;
        public StayRepository stayRepository;
        public PlaceRepository placeRepository;

        public AdminStayEatDetailPage(Place place, bool isStay)
        {
            InitializeComponent();
            Place = place;
            DataContext = new StayEatViewModel(place);

            dbContext = new TravelAgencyContext();
            stayRepository = new StayRepository(dbContext);
            placeRepository = new PlaceRepository(dbContext);
            string curDir = Directory.GetCurrentDirectory();

            int index = 1;
            for (int i = 0; i < 3; i++)
            {
                index = curDir.LastIndexOf('\\');
                if (index == -1)
                    break;
                curDir = curDir.Substring(0, index);
            }
            curDir += "\\Resources\\Images\\";
            ImageDirectory = curDir + place.Id + "\\";
            try
            {
                ImageNames = Directory.GetFiles(ImageDirectory);
            }
            catch(DirectoryNotFoundException e)
            {
                ImageDirectory = curDir;
                ImageNames = Directory.GetFiles(ImageDirectory);
            }
            
            CurrentImageIndex = 0;
            var viewModel = DataContext as StayEatViewModel;
            if (viewModel != null)
            {
                viewModel.FrontImageSource = ImageNames[CurrentImageIndex];
                viewModel.BackImageSource = ImageNames[CurrentImageIndex];
                
                if (isStay)
                {
                    AmenitiesSegment.Visibility = Visibility.Visible;
                    Place = stayRepository.GetById(Place.Id);
                    RefreshAmenities();
                } else
                {
                    AmenitiesSegment.Visibility = Visibility.Collapsed;
                }
                
                
            }

            DrawPin();
            Loaded += Page_Loaded;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            FocusManager.SetFocusedElement(this, editButton);
            Keyboard.Focus(this);
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
                Place.lng = point.GetX();
                Place.lat = point.GetY();

                placeRepository.Update(Place);
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
                if (Snackbar.MessageQueue is { } messageQueue)
                {
                    var message = "Place edited successfully";
                    messageQueue.Enqueue(message);
                }
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
                if (Snackbar.MessageQueue is { } messageQueue)
                {
                    var message = "Amenities refreshed";
                    messageQueue.Enqueue(message);
                }
            }
        }

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

        private void Edit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            EditPlaceDialog editPlaceDialog = new EditPlaceDialog(Place);

            editPlaceDialog.DialogResultEvent += EditPlace_DialogResultEvent;

            editPlaceDialog.ShowDialog();
        }
    }
}