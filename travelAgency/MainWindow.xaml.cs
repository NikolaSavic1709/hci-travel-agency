using DevExpress.Xpf.Map;
using System;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace travelAgency
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            using (var db = new TravelAgencyContext())
            {
                db.Database.EnsureCreated();
                db.SaveChanges();
                //int journeyId = 1; // Specify the journey ID
                //Journey journey = db.Journeys.Include(j => j.JourneyPlaces)
                //    .ThenInclude(jp=>jp.Place)
                //   .FirstOrDefault(j => j.Id == journeyId);

                //if (journey != null)
                //{
                //    List<Place> places = journey.JourneyPlaces.Select(jp => jp.Place).ToList();

                //}
            }
            var pin = new MapPushpin();

            //pin.MarkerTemplate = (DataTemplate)TryFindResource("pushpin");
            pin.Location = new GeoPoint(41.88, -87.63);
            pin.Information = "Test Chicago Information";
            pin.LocationChangedAnimation = new PushpinLocationAnimation();
            pin.CanMove = true;
            mapItems.Items.Add(pin);
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
    }
}