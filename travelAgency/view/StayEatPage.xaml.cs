using DevExpress.Xpf.Map;
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
/// Interaction logic for StayEatPage.xaml
/// </summary>
public partial class StayEatPage : Page
{
    public TravelAgencyContext dbContext;
    public RestaurantRepository restaurantRepository;
    public StayRepository stayRepository;
    public string BingKey { get; set; }
    private MapPushpin pin;

    public StayEatPage()
    {
        BingKey = "Bobfc3eHAUPXgpeTYjms~m6tD9dbiJz0HFBraHXWR_A~AvjPgt_MJZVhNNdKhscJbncrArt9ydMHTLueaT6sDhboip1smAQSMs7436fWsrtq";
        InitializeComponent();
        dbContext = new TravelAgencyContext();
        restaurantRepository = new RestaurantRepository(dbContext);
        stayRepository = new StayRepository(dbContext);

        List<Restaurant> restaurants = restaurantRepository.GetAll();
        foreach (Restaurant p in restaurants)
        {
            StayEatCard stayEatCard = new StayEatCard
            {
                Margin = new Thickness(10),
                StayEat = p
            };
            stayEatCard.ToStayEatClicked += StayEatCard_ToStayEat;
            cards.Children.Add(stayEatCard);
        }

        List<Stay> stays = stayRepository.GetAll();
        foreach (Stay p in stays)
        {
            StayEatCard stayEatCard = new StayEatCard
            {
                Margin = new Thickness(10),
                StayEat = p
            };
            stayEatCard.ToStayEatClicked += StayEatCard_ToStayEat;
            cards.Children.Add(stayEatCard);
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

        place2.lat = 44.66278;
        place2.lng = 20.93;

        Restaurant restaurant = new Restaurant();
        restaurant.Name = "Vila Jugovo";
        restaurant.lat = 44.66278;
        restaurant.lng = 20.93;

        StayEatCard placeCard1 = new StayEatCard
        {
            Margin = new Thickness(10),
            StayEat = restaurant
        };
        placeCard1.ToStayEatClicked += StayEatCard_ToStayEat;
        placeCard1.MouseDown += PlaceCard1_MouseDown;
        cards.Children.Add(placeCard1);

        Stay stay = new Stay();
        stay.Name = "Saviceva kuca";
        stay.lat = 44.7553;
        stay.lng = 19.6923;

        StayEatCard placeCard2 = new StayEatCard
        {
            Margin = new Thickness(10),
            StayEat = stay
        };
        placeCard2.ToStayEatClicked += StayEatCard_ToStayEat;
        placeCard2.MouseDown += PlaceCard1_MouseDown;
        cards.Children.Add(placeCard2);
    }

    private void PlaceCard1_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (pin != null)
            mapItems.Items.Remove(pin);

        Place p = ((StayEatCard)e.Source).StayEat;
        pin = new MapPushpin();

        //pin.MarkerTemplate = (DataTemplate)TryFindResource("pushpin");
        pin.Location = new GeoPoint(p.lat, p.lng);
        pin.Information = p.Name;
        pin.LocationChangedAnimation = new PushpinLocationAnimation();
        pin.CanMove = false;
        mapItems.Items.Add(pin);
    }

    private void StayEatCard_ToStayEat(object sender, ToStayEatEventArgs e)
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
        CreatePlaceDialog window = new CreatePlaceDialog();
        window.ShowDialog();
    }
}