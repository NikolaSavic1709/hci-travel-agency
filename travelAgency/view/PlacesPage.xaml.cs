using DevExpress.Map.Kml.Model;
using DevExpress.Xpf.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using travelAgency.Commands;
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
    public List<AttractionCard> placeCards;
    public List<AttractionCard> filteredPlaceCards;
    public ICommand SearchCommand { get; }
    public PlacesPage()
    {
        BingKey = "Bobfc3eHAUPXgpeTYjms~m6tD9dbiJz0HFBraHXWR_A~AvjPgt_MJZVhNNdKhscJbncrArt9ydMHTLueaT6sDhboip1smAQSMs7436fWsrtq";
        SearchCommand = new CommandImplementationcs(Search);
        placeCards = new List<AttractionCard>();
        InitializeComponent();
        DataContext = this;

        dbContext = new TravelAgencyContext();
        attractionRepository = new AttractionRepository(dbContext);

        List<Attraction> attractions = attractionRepository.GetAll();
        foreach (Attraction a in attractions)
        {
            CreateCard(a);
        }

        RefreshCards(false);
        Loaded += Page_Loaded;
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        FocusManager.SetFocusedElement(this, AddBtn);
        Keyboard.Focus(this);
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
        placeCards.Add(placeCard);
    }

    public void RefreshCards(bool isFilter)
    {
        cards.Children.Clear();
        if (isFilter)
        {
            foreach (AttractionCard a in filteredPlaceCards)
            {
                cards.Children.Add(a);
            }
        }
        else
        {
            foreach (AttractionCard a in placeCards)
            {
                cards.Children.Add(a);
            }
        }

    }
    private void AttractionCard_NewAttraction(object sender, ToAttractionEventArgs e)
    {
        CreateCard(e.Attraction);
        RefreshCards(false);
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
        RemoveIfExists(card);
        attractionRepository.Delete(attraction);

    }

    public void RemoveIfExists(AttractionCard card)
    {
        if (placeCards.Contains(card))
        {
            placeCards.Remove(card);
        }
        if (filteredPlaceCards!=null)
            if (filteredPlaceCards.Contains(card))
            {
                filteredPlaceCards.Remove(card);
            }
        
    }

    private void Search_OnKeyDown(object sender, KeyEventArgs e)
    {
        var textBox = (TextBox)sender;
        if (e.Key == Key.Enter)
            SearchButton.Command.Execute(textBox.Text);
    }

    private async void Search(object? obj)
    {
        var text = obj as string;
        if (string.IsNullOrWhiteSpace(text))
        {
            filteredPlaceCards = placeCards;
        }
        else
        {
            filteredPlaceCards = await Task.Run(() => placeCards
                .Where(x => x.AttractionName.IndexOf(text, StringComparison.CurrentCultureIgnoreCase) >= 0)
                .ToList());
        }
        RefreshCards(true);
    }

    private MapPushpin mapItem;

    private void CreatePlace_Click(object sender, RoutedEventArgs e)
    {
        CreatePlaceDialog window = new CreatePlaceDialog(true);
        window.NewAttraction += AttractionCard_NewAttraction;
        window.ShowDialog();
    }

    private void NewCommandExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        CreatePlaceDialog window = new CreatePlaceDialog(true);
        window.NewAttraction += AttractionCard_NewAttraction;
        window.ShowDialog();
    }

    private void SearchCommandExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        SearchBox.Focus();
    }
}