﻿using DevExpress.Xpf.Map;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using travelAgency.components;
using travelAgency.Dialogs;
using travelAgency.model;
using travelAgency.repository;

namespace travelAgency.view;

/// <summary>
/// Interaction logic for PlacesPage.xaml
/// </summary>
public partial class PlacesPage : Page
{
    public TravelAgencyContext dbContext;
    public AttractionRepository attractionRepository;
    public string BingKey { get; set; }
    private MapPushpin pin;

    public PlacesPage()
    {
        BingKey = "Bobfc3eHAUPXgpeTYjms~m6tD9dbiJz0HFBraHXWR_A~AvjPgt_MJZVhNNdKhscJbncrArt9ydMHTLueaT6sDhboip1smAQSMs7436fWsrtq";

        InitializeComponent();

        dbContext = new TravelAgencyContext();
        attractionRepository = new AttractionRepository(dbContext);

        List<Attraction> attractions = attractionRepository.GetAll();
        foreach (Attraction a in attractions)
        {
            CreateCard(a);
        }

        
    }
    private void CreateCard(Attraction attraction)
    {
        AttractionCard placeCard = new AttractionCard
        {
            Margin = new Thickness(10),
            Attraction = attraction
        };
        placeCard.ToAttractionClicked += AttractionCard_ToPlace;
        placeCard.AttractionDelete += AttractionCard_Remove;
        placeCard.MouseDown += AttractionCard_MouseDown;
        cards.Children.Add(placeCard);
    }
    private void AttractionCard_NewAttraction(object sender, ToAttractionEventArgs e)
    {
        CreateCard(e.Attraction);
    }
    private void AttractionCard_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (pin != null)
            mapItems.Items.Remove(pin);

        Place p = ((AttractionCard)e.Source).Attraction;
        pin = new MapPushpin();

        //pin.MarkerTemplate = (DataTemplate)TryFindResource("pushpin");
        pin.Location = new GeoPoint(p.lat, p.lng);
        pin.Information = p.Name;
        pin.LocationChangedAnimation = new PushpinLocationAnimation();
        pin.CanMove = false;
        mapItems.Items.Add(pin);
    }

    private void AttractionCard_ToPlace(object sender, ToAttractionEventArgs e)
    {
        Attraction attraction = e.Attraction;

        NavigationService?.Navigate(new AdminStayEatDetailPage(attraction, false));
    }
    private void AttractionCard_Remove(object sender, ToAttractionEventArgs e)
    {
        Attraction attraction = e.Attraction;
        AttractionCard card = (AttractionCard)sender;
        cards.Children.Remove(card);
        attractionRepository.Delete(attraction);

    }
    private void Search_OnKeyDown(object sender, KeyEventArgs e)
    {
        var textBox = (TextBox)sender;
        if (e.Key == Key.Enter)
            SearchButton.Command.Execute(textBox.Text);
    }

    private MapPushpin mapItem;

    private void CreatePlace_Click(object sender, RoutedEventArgs e)
    {
        CreatePlaceDialog window = new CreatePlaceDialog(true);
        window.NewAttraction += AttractionCard_NewAttraction;
        window.ShowDialog();
    }

}