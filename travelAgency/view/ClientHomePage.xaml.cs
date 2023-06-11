using DevExpress.Xpf.Map;
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
/// Interaction logic for ClientHomePage.xaml
/// </summary>
public partial class ClientHomePage : Page
{
    public ClientHomePage()
    {

        BingKey = "";
        InitializeComponent();
        dbContext = new TravelAgencyContext();
        tripRepository = new TripRepository(dbContext);

        List<Trip> trips = tripRepository.GetAll();
        foreach (Trip t in trips)
        {
            TripCard tripCard = new TripCard
            {
                Margin = new Thickness(10),
                Place = t

            };
            tripCard.ToTripClicked += TripCard_ToTrip;
            cards.Children.Add(tripCard);
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

        ClientTripCard tripCard1 = new ClientTripCard
        {
            Margin = new Thickness(10),
            Trip = trip
        };
        tripCard1.ToTripClicked += TripCard_ToTrip;

        //TripCard tripCard2 = new TripCard
        //{
        //    Margin = new Thickness(10),
        //    TripName = "Planinski maratoni",
        //    Route = "Raška - Pančićev vrh",
        //    Description = "Tura je veoma zaniljiva i duga jer Savic i drugari imaju sta da ponude i njihovom kraju"
        //};
        //tripCard2.ToTripClicked += TripCard_ToTrip;

        //TripCard tripCard3 = new TripCard
        //{
        //    Margin = new Thickness(10),
        //    TripName = "Tura južna Srbija",
        //    Route = "Vranje - Đavolja Varoš",
        //    Description = "Ubedljiva najbolja tura u nasoj ponudi"
        //};
        //tripCard3.ToTripClicked += TripCard_ToTrip;

        cards.Children.Add(tripCard1);
        //cards.Children.Add(tripCard2);
        //cards.Children.Add(tripCard3);
    }

    public TravelAgencyContext dbContext;
    public TripRepository tripRepository;
    public string BingKey { get; set; }

    private void TripCard_ToTrip(object sender, ToTripEventArgs e)
    {
        Trip trip = e.Trip;

        NavigationService?.Navigate(new TripDetailsPage(trip));
    }
    private void Search_OnKeyDown(object sender, KeyEventArgs e)
    {
        var textBox = (TextBox)sender;
        if (e.Key == Key.Enter)
            SearchButton.Command.Execute(textBox.Text);
    }
    private void geocodeProvider_LocationInformationReceived(object sender, LocationInformationReceivedEventArgs e)
    {

        GeocodeRequestResult result = e.Result;
        StringBuilder resultList = new StringBuilder("");
        resultList.Append(String.Format("Status: {0}\n", result.ResultCode));
        resultList.Append(String.Format("Fault reason: {0}\n", result.FaultReason));
        resultList.Append(String.Format("______________________________\n"));

        if (result.ResultCode != RequestResultCode.Success)
        {
            tbResults.Text = resultList.ToString();
            return;
        }

        int resCounter = 1;
        foreach (LocationInformation locations in result.Locations)
        {
            resultList.Append(String.Format("Request Result {0}:\n", resCounter));
            resultList.Append(String.Format("Display Name: {0}\n", locations.DisplayName));
            resultList.Append(String.Format("Entity Type: {0}\n", locations.EntityType));
            resultList.Append(String.Format("Address: {0}\n", locations.Address));
            resultList.Append(String.Format("Location: {0}\n", locations.Location));
            resultList.Append(String.Format("______________________________\n"));
            resCounter++;
        }

        tbResults.Text = resultList.ToString();
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

    private void CreateTrip_Click(object sender, RoutedEventArgs e)
    {
        CreateTripDialog window = new CreateTripDialog();
        window.ShowDialog();
    }
}
