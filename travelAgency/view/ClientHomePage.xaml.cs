﻿using DevExpress.Xpf.Map;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using travelAgency.components;
using travelAgency.Dialogs;
using travelAgency.model;
using travelAgency.repository;

namespace travelAgency.view;

/// <summary>
/// Interaction logic for ClientHomePage.xaml
/// </summary>
public partial class ClientHomePage : Page
{

    public TravelAgencyContext dbContext;
    public TripRepository tripRepository;
    public PlaceRepository placeRepository;
    public ClientHomePage()
    {
        BingKey = "";
        InitializeComponent();
        dbContext = new TravelAgencyContext();
        tripRepository = new TripRepository(dbContext);
        placeRepository = new PlaceRepository(dbContext);
        List<Trip> trips = tripRepository.GetAll();


        foreach (Trip t in trips)
        {
            CreateCard(t);
        }
    }
    private void CreateCard(Trip trip)
    {
        ClientTripCard tripCard = new ClientTripCard
        {
            Margin = new Thickness(10),
            Trip = trip

        };
        tripCard.ToTripClicked += TripCard_ToTrip;
        tripCard.MouseDown += TripCard_MouseDown;
        cards.Children.Add(tripCard);
    }

    private void TripCard_MouseDown(object sender, MouseButtonEventArgs e)
    {
        Trip trip = (sender as ClientTripCard).Trip;
        List<RouteWaypoint> waypoints = new List<RouteWaypoint>();
        foreach (var schedule in trip.Schedules)
        {
            waypoints.Add(new RouteWaypoint(schedule.Place.Name, new GeoPoint(schedule.Place.lat, schedule.Place.lng)));
        }
        routeProvider.CalculateRoute(waypoints);
    }

    public string BingKey { get; set; }

    private void TripCard_ToTrip(object sender, ToTripEventArgs e)
    {
        Trip trip = e.Trip;

        NavigationService?.Navigate(new TripDetailsPage(trip,tripRepository,placeRepository));
    }

    private void Search_OnKeyDown(object sender, KeyEventArgs e)
    {
        var textBox = (TextBox)sender;
        if (e.Key == Key.Enter)
            SearchButton.Command.Execute(textBox.Text);
    }

    private void geocodeProvider_LocationInformationReceived(object sender, LocationInformationReceivedEventArgs e)
    {
        //GeocodeRequestResult result = e.Result;
        //StringBuilder resultList = new StringBuilder("");
        //resultList.Append(String.Format("Status: {0}\n", result.ResultCode));
        //resultList.Append(String.Format("Fault reason: {0}\n", result.FaultReason));
        //resultList.Append(String.Format("______________________________\n"));

        //if (result.ResultCode != RequestResultCode.Success)
        //{
        //    tbResults.Text = resultList.ToString();
        //    return;
        //}

        //int resCounter = 1;
        //foreach (LocationInformation locations in result.Locations)
        //{
        //    resultList.Append(String.Format("Request Result {0}:\n", resCounter));
        //    resultList.Append(String.Format("Display Name: {0}\n", locations.DisplayName));
        //    resultList.Append(String.Format("Entity Type: {0}\n", locations.EntityType));
        //    resultList.Append(String.Format("Address: {0}\n", locations.Address));
        //    resultList.Append(String.Format("Location: {0}\n", locations.Location));
        //    resultList.Append(String.Format("______________________________\n"));
        //    resCounter++;
        //}

        //tbResults.Text = resultList.ToString();
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
}