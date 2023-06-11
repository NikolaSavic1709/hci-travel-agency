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
/// Interaction logic for PlacesPage.xaml
/// </summary>
public partial class PlacesPage : Page
{
    public TravelAgencyContext dbContext;
    public PlaceRepository placeRepository;
    public string BingKey { get; set; }
    private MapPushpin pin;

    public PlacesPage()
    {
        BingKey = "Bobfc3eHAUPXgpeTYjms~m6tD9dbiJz0HFBraHXWR_A~AvjPgt_MJZVhNNdKhscJbncrArt9ydMHTLueaT6sDhboip1smAQSMs7436fWsrtq";

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
            placeCard.MouseDown += PlaceCard1_MouseDown;
            cards.Children.Add(placeCard);
        }

        
    }

    private void PlaceCard1_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (pin != null)
            mapItems.Items.Remove(pin);

        Place p = ((PlaceCard)e.Source).Place;
        pin = new MapPushpin();

        //pin.MarkerTemplate = (DataTemplate)TryFindResource("pushpin");
        pin.Location = new GeoPoint(p.lat, p.lng);
        pin.Information = p.Name;
        pin.LocationChangedAnimation = new PushpinLocationAnimation();
        pin.CanMove = false;
        mapItems.Items.Add(pin);
    }

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
        CreatePlaceDialog window = new CreatePlaceDialog();
        window.ShowDialog();
    }
}