using DevExpress.Xpf.Map;
using MaterialDesignColors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using travelAgency.Commands;
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
    public ArrangementRepository arrangementRepository;
    public List<ClientTripCard> tripCards;
    public List<ClientTripCard> searchTripCards;
    public List<ClientTripCard> filteredTripCards;
    private User loggedUser;

    public ICommand SearchCommand { get; }

    public ClientHomePage(TravelAgencyContext dbContext, User loggedUser)
    {
        BingKey = "";
        SearchCommand = new CommandImplementationcs(Search);
        tripCards = new List<ClientTripCard>();
        filteredTripCards = new List<ClientTripCard>();
        InitializeComponent();
        DataContext = this;
        this.dbContext = dbContext;
        tripRepository = new TripRepository(dbContext);
        placeRepository = new PlaceRepository(dbContext);
        arrangementRepository = new ArrangementRepository(dbContext);
        this.loggedUser = loggedUser;
        List<Trip> trips = tripRepository.GetAll();

        foreach (Trip t in trips)
        {
            CreateCard(t);
        }

        RefreshCards(false);
        Loaded += Page_Loaded;
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        FocusManager.SetFocusedElement(this, Filter_Btn);
        Keyboard.Focus(this);
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
        tripCards.Add(tripCard);
        filteredTripCards.Add(tripCard);
    }

    public void RefreshCards(bool isFilter)
    {
        cards.Children.Clear();
        if (isFilter)
        {
            foreach (ClientTripCard a in searchTripCards)
            {
                cards.Children.Add(a);
            }
        }
        else
        {
            foreach (ClientTripCard a in filteredTripCards)
            {
                cards.Children.Add(a);
            }
        }
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

        NavigationService?.Navigate(new ClientTripDetailsPage(trip, dbContext, loggedUser));
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
            searchTripCards = filteredTripCards;
        }
        else
        {
            searchTripCards = await Task.Run(() => filteredTripCards
                .Where(x => x.TripName.IndexOf(text, StringComparison.CurrentCultureIgnoreCase) >= 0)
                .ToList());
        }
        RefreshCards(true);
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

    private void Filter_Click(object sender, RoutedEventArgs e)
    {
        SearchBox.Text = "";
        FilterClientTripDialog filterTripDialog = new FilterClientTripDialog(tripCards);

        filterTripDialog.DialogResultEvent += Filter_DialogResultEvent;

        filterTripDialog.ShowDialog();
    }

    private void Filter_DialogResultEvent(object sender, ClientTripCardEventArgs e)
    {
        List<ClientTripCard> result = e.ClientTripCards;

        if (result != null)
        {
            filteredTripCards = result;
        }
        RefreshCards(false);
    }

    private void SearchCommandExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        SearchBox.Focus();
    }

    private void FilterCommandExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        SearchBox.Text = "";
        FilterClientTripDialog filterTripDialog = new FilterClientTripDialog(tripCards);

        filterTripDialog.DialogResultEvent += Filter_DialogResultEvent;

        filterTripDialog.ShowDialog();
    }
}