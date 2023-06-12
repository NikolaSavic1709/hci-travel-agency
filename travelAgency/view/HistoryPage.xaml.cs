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
using MaterialDesignThemes.Wpf;
using travelAgency.Commands;

namespace travelAgency.view
{
    /// <summary>
    /// Interaction logic for HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : Page
    {
        public TravelAgencyContext dbContext;
        public ArrangementRepository arrangementRepository;
        public List<Arrangement> arrangements;
        public List<ArrangementCard> arrangementCards;
        public List<ArrangementCard> searchArrangementCards;
        public List<ArrangementCard> filteredArrangementCards;
        public ICommand SearchCommand { get; }

        public HistoryPage(User loggedUser)
        {
            SearchCommand = new CommandImplementationcs(Search);
            arrangementCards = new List<ArrangementCard>();
            filteredArrangementCards = new List<ArrangementCard>();
            InitializeComponent();
            DataContext = this;
            dbContext = new TravelAgencyContext();
            arrangementRepository = new ArrangementRepository(dbContext);
            
            if (loggedUser == null)
            {
                arrangements = arrangementRepository.GetAll();
            } else
            {
                arrangements = arrangementRepository.GetArrangementsForUser(loggedUser.Id);
            }
           
            foreach (Arrangement a in arrangements)
            {
                ArrangementCard arrangementCard = new ArrangementCard
                {
                    Margin = new Thickness(10),
                    Arrangement = a

                };
                arrangementCards.Add(arrangementCard);
                filteredArrangementCards.Add(arrangementCard);
            }
            RefreshCards(false);
            
        }

        public void RefreshCards(bool isFilter)
        {
            cards.Children.Clear();
            if (isFilter)
            {             
                foreach (ArrangementCard a in searchArrangementCards)
                {
                    cards.Children.Add(a);
                }
            } else
            {
                foreach (ArrangementCard a in filteredArrangementCards)
                {
                    cards.Children.Add(a);
                }
            }
            
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
              searchArrangementCards = filteredArrangementCards;
            }
            else
            {
                searchArrangementCards = await Task.Run(() => filteredArrangementCards
                    .Where(x => x.Trip.Name.IndexOf(text, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    .ToList());
            }
            RefreshCards(true);
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            SearchBox.Text = "";
            FilterArrangementDialog filterArrangementDialog = new FilterArrangementDialog(arrangementCards);

            filterArrangementDialog.DialogResultEvent += Filter_DialogResultEvent;

            filterArrangementDialog.ShowDialog();
        }

        private void Filter_DialogResultEvent(object sender, ArrangementCardEventArgs e)
        {
            List<ArrangementCard> result = e.ArrangementCards;

            if (result!=null)
            {
               filteredArrangementCards = result;
            }
            RefreshCards(false);
        }
    }
}