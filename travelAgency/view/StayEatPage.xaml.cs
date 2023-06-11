using DevExpress.Pdf.Native.BouncyCastle.Asn1.X509.Qualified;
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
            CreateCard(p);
        }

        List<Stay> stays = stayRepository.GetAll();
        foreach (Stay p in stays)
        {
            CreateCard(p);
        }

    }
    private void CreateCard(Place place)
    {
        StayEatCard placeCard = new StayEatCard
        {
            Margin = new Thickness(10),
            StayEat=place
        };
        placeCard.ToStayEatClicked += StayEatCard_ToStayEat;
        placeCard.StayEatDelete += StayEatCard_Remove;
        placeCard.MouseDown += StayEatCard_MouseDown;
        cards.Children.Add(placeCard);
    }
    private void StayEatCard_NewStayEat(object sender, ToStayEatEventArgs e)
    {
        CreateCard(e.Place);
    }
    private void StayEatCard_MouseDown(object sender, MouseButtonEventArgs e)
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
        if (place is Stay)
            NavigationService?.Navigate(new AdminStayEatDetailPage(place, true));
        else
            NavigationService?.Navigate(new AdminStayEatDetailPage(place, false));
    }
    private void StayEatCard_Remove(object sender, ToStayEatEventArgs e)
    {
        Place place = e.Place;
        
        StayEatCard card = (StayEatCard)sender;
        cards.Children.Remove(card);
        if (place is Restaurant)
            restaurantRepository.Delete((Restaurant)place);
        else
            stayRepository.Delete((Stay)place);

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
        CreatePlaceDialog window = new CreatePlaceDialog();
        window.NewStayEat += StayEatCard_NewStayEat;
        window.ShowDialog();
    }
}