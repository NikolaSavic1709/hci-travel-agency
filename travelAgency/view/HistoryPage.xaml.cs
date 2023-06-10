using DevExpress.Xpf.Map;
using DevExpress.XtraPrinting.Native;
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
using travelAgency.Dialogs;
using travelAgency.model;
using travelAgency.repository;
using travelAgency.components;

namespace travelAgency.view
{
    /// <summary>
    /// Interaction logic for HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : Page
    {
        public TravelAgencyContext dbContext;
        public ArrangementRepository arrangementRepository;
        public HistoryPage()
        {
            InitializeComponent();
            dbContext = new TravelAgencyContext();
            arrangementRepository = new ArrangementRepository(dbContext);

            List<Arrangement> arrangements = arrangementRepository.GetAll();
            foreach (Arrangement a in arrangements)
            {
                ArrangementCard arrangementCard = new ArrangementCard
                {
                    Margin = new Thickness(10),
                    Arrangement = a

                };
                cards.Children.Add(arrangementCard);
            }

            Trip trip = new Trip();
            trip.Price = 3200;
            trip.Name = "Tura zapadna Srbija";
            User user = new User("Miki","miki@gmail.com","mikkkki","Milane");
            Arrangement arrangement = new Arrangement();
            arrangement.User = user;
            arrangement.Trip = trip;
            arrangement.numberOfPearsons = 7;
            arrangement.IsReservation = true;
            arrangement.DateTime = DateTime.Now;

            ArrangementCard arrangementCard1 = new ArrangementCard
            {
                Margin = new Thickness(10),
                Arrangement = arrangement
            };
            
            cards.Children.Add(arrangementCard1);

            Trip trip2 = new Trip();
            trip2.Price = 900;
            trip2.Name = "Tura zapadna Srbija";
            User user2 = new User("Miki", "miki@gmail.com", "mikkkki", "Milane");
            Arrangement arrangement2 = new Arrangement();
            arrangement2.User = user2;
            arrangement2.Trip = trip2;
            arrangement2.numberOfPearsons = 4;
            arrangement2.IsReservation = false;
            arrangement2.DateTime = DateTime.Now;

            ArrangementCard arrangementCard2 = new ArrangementCard
            {
                Margin = new Thickness(10),
                Arrangement = arrangement2
            };

            cards.Children.Add(arrangementCard2);
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

    }
}
