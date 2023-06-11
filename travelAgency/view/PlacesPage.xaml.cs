using DevExpress.Xpf.Map;
using Microsoft.EntityFrameworkCore;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
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
    public PlacesPage()
    {
        BingKey = "";

        InitializeComponent();

        dbContext = new TravelAgencyContext();
        placeRepository = new PlaceRepository(dbContext);

        List<Place> places = placeRepository.GetAll();
        foreach (Place p in places)
        {
            PlaceCard placeCard = new PlaceCard
            {
                Margin = new Thickness(10),
                Place = p

            };
            placeCard.ToPlaceClicked += PlaceCard_ToPlace;
            cards.Children.Add(placeCard);
        }

        Trip trip = new Trip();
        trip.Name = "Tura zapadna Srbija";
        trip.Description = "Tura je veoma zaniljiva i duga jer Savic i drugari imaju sta da ponude i njihovom kraju";
        Place place = new Place();
        place.Name = "Vranje";
        Place place2 = new Place();
        place2.Name = "Smederevo";
        TripSchedule tripSchedule = new TripSchedule();
        tripSchedule.Place = place;
        TripSchedule tripSchedule2 = new TripSchedule();
        tripSchedule2.Place = place2;

        trip.Schedules.Add(tripSchedule);
        trip.Schedules.Add(tripSchedule2);
        place.lat = 44.6620;
        place2.lng = 20.9302;

        PlaceCard placeCard1 = new PlaceCard
        {
            Margin = new Thickness(10),
            Place = place2
        };
        placeCard1.ToPlaceClicked += PlaceCard_ToPlace;
        placeCard1.MouseDown += PlaceCard1_MouseDown;
        cards.Children.Add(placeCard1);


    }

    private void PlaceCard1_MouseDown(object sender, MouseButtonEventArgs e)
    {
        Place p = ((PlaceCard)e.Source).Place;
        
    }

    public TravelAgencyContext dbContext;
    public PlaceRepository placeRepository;
    public string BingKey { get; set; }


    private void PlaceCard_ToPlace(object sender, ToPlaceEventArgs e)
    {
        Place place = e.Place;

        //NavigationService?.Navigate(new TripDetailsPage(place));
    }
    private void Search_OnKeyDown(object sender, KeyEventArgs e)
    {
        var textBox = (TextBox)sender;
        if (e.Key == Key.Enter)
            SearchButton.Command.Execute(textBox.Text);
    }

    private MapPushpin mapItem;


    private void CreateTrip_Click(object sender, RoutedEventArgs e)
    {
        CreateTripDialog window = new CreateTripDialog();
        window.ShowDialog();
    }
}
