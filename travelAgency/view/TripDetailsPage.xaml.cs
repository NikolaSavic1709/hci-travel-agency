﻿using DevExpress.Xpf.Map;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using travelAgency.components;
using travelAgency.Dialogs;
using travelAgency.model;
using travelAgency.repository;
using travelAgency.ViewModel;

namespace travelAgency.view
{
    /// <summary>
    /// Interaction logic for TripDetailsPage.xaml
    /// </summary>
    public partial class TripDetailsPage : Page
    {
        private Trip Trip { get; set; }
        private TripDetailsViewModel? ViewModel { get; set; }

        public TripRepository tripRepository;
        private PlaceRepository placeRepository;

        public TripDetailsPage(Trip trip, TripRepository tripRepository, PlaceRepository placeRepository)
        {
            InitializeComponent();
            this.tripRepository = tripRepository;
            this.placeRepository = placeRepository;

            Trip = trip;
            DataContext = new TripDetailsViewModel();

            ViewModel = DataContext as TripDetailsViewModel;
            if (ViewModel != null)
            {
                ViewModel.Trip = trip;
            }
            DrawRoute();
            Loaded += Page_Loaded;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            FocusManager.SetFocusedElement(this, EditBtn);
            Keyboard.Focus(this);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            CreateTripScheduleDialog dialog = new CreateTripScheduleDialog(Trip, tripRepository, placeRepository, null);
            dialog.DialogResultEvent += AddTripSchedule_Result;
            dialog.ShowDialog();
            
        }

        private void AddTripSchedule_Result(object? sender, DialogResultEventArgs e)
        {
            if (Snackbar.MessageQueue is { } messageQueue)
            {
                var message = "Place added successfully";
                messageQueue.Enqueue(message);
            }
            DrawRoute();
        }

        private void RemovePlace_Click(object sender, RoutedEventArgs e)
        {
            Button removeButton = (Button)sender;
            TripSchedule tripSchedule = (TripSchedule)removeButton.DataContext;
            if (ViewModel != null)
            {
                if (ViewModel.Trip.Schedules.Count > 1)
                {
                    ViewModel.Trip.Schedules.Remove(tripSchedule);
                    Trip.Schedules.Remove(tripSchedule);
                    this.tripRepository.Save();
                    if (Snackbar.MessageQueue is { } messageQueue)
                    {
                        var message = "Schedule removed successfully";
                        messageQueue.Enqueue(message);
                    }
                }
                else
                {
                    if (Snackbar.MessageQueue is { } messageQueue)
                    {
                        var message = "Tour must have at least one place";
                        messageQueue.Enqueue(message);
                    }
                }
            }
            DrawRoute();
        }

        private void EditPlace_Click(object sender, RoutedEventArgs e)
        {
            Button editButton = (Button)sender;
            TripSchedule tripSchedule = (TripSchedule)editButton.DataContext;

            CreateTripScheduleDialog dialog = new CreateTripScheduleDialog(Trip, tripRepository, placeRepository, tripSchedule);
            dialog.DialogResultEvent += EditTripSchedule_Result;
            dialog.ShowDialog();
        }

        private void EditTripSchedule_Result(object? sender, DialogResultEventArgs e)
        {
            if (Snackbar.MessageQueue is { } messageQueue)
            {
                var message = "Schedule edited successfully";
                messageQueue.Enqueue(message);
            }
        }

        private void EditTour_Click(object sender, RoutedEventArgs e)
        {
            TourEdit dialog = new TourEdit(Trip, tripRepository);
            dialog.DialogResultEvent += EditTrip_Result;
            dialog.ShowDialog();
        }

        private void EditTrip_Result(object? sender, DialogResultEventArgs e)
        {
            if (Snackbar.MessageQueue is { } messageQueue)
            {
                var message = "Trip edited successfully";
                messageQueue.Enqueue(message);
            }
        }

        private void routeProvider_LayerItemsGenerating(object sender, LayerItemsGeneratingEventArgs args)
        {
            char letter = 'A';
            foreach (MapItem item in args.Items)
            {
                MapPushpin pushpin = item as MapPushpin;
                if (pushpin != null)
                    pushpin.Text = letter++.ToString();
                MapPolyline line = item as MapPolyline;
                if (line != null)
                {
                    var converter = new System.Windows.Media.BrushConverter();
                    var brush = (Brush)converter.ConvertFromString("#009882");
                    line.Fill = brush;
                    line.Stroke = brush;
                }
            }

            map.ZoomToFit(args.Items);
        }

        public void DrawRoute()
        {
            List<RouteWaypoint> waypoints = new List<RouteWaypoint>();
            foreach (var schedule in Trip.Schedules)
            {
                waypoints.Add(new RouteWaypoint(schedule.Place.Name, new GeoPoint(schedule.Place.lat, schedule.Place.lng)));
            }
            routeProvider.CalculateRoute(waypoints);
        }

        private void Edit_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            TourEdit dialog = new TourEdit(Trip, tripRepository);
            dialog.ShowDialog();
        }

        private void AddPlace_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            CreateTripScheduleDialog dialog = new CreateTripScheduleDialog(Trip, tripRepository, placeRepository, null);
            dialog.ShowDialog();
            DrawRoute();
        }
    }
}