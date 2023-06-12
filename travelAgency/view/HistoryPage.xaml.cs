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
        public List<ArrangementCard> filteredArrangementCards;
        public ICommand SearchCommand { get; }

        public HistoryPage(User loggedUser)
        {
            SearchCommand = new CommandImplementationcs(Search);
            arrangementCards = new List<ArrangementCard>();
            InitializeComponent();
            DataContext = this;
            dbContext = new TravelAgencyContext();
            arrangementRepository = new ArrangementRepository(dbContext);

            if (loggedUser == null)
            {
                arrangements = arrangementRepository.GetAll();
            }
            else
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
                arrangementCard.MouseDown += ArrangementCard_MouseDown;
                arrangementCards.Add(arrangementCard);
            }
            RefreshCards(false);
        }

        private void ArrangementCard_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DrawRoute((sender as ArrangementCard).Arrangement.Trip);
        }

        public void RefreshCards(bool isFilter)
        {
            cards.Children.Clear();
            if (isFilter)
            {
                foreach (ArrangementCard a in filteredArrangementCards)
                {
                    cards.Children.Add(a);
                }
            }
            else
            {
                foreach (ArrangementCard a in arrangementCards)
                {
                    cards.Children.Add(a);
                }
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
                filteredArrangementCards = arrangementCards;
            }
            else
            {
                filteredArrangementCards = await Task.Run(() => arrangementCards
                    .Where(x => x.Trip.Name.IndexOf(text, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    .ToList());
            }
            RefreshCards(true);
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            FilterArrangementDialog filterArrangementDialog = new FilterArrangementDialog();

            filterArrangementDialog.DialogResultEvent += Filter_DialogResultEvent;

            filterArrangementDialog.ShowDialog();
        }

        private void Filter_DialogResultEvent(object sender, DialogResultEventArgs e)
        {
            bool result = e.Result;

            if (result)
            {
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

        public void DrawRoute(Trip trip)
        {
            List<RouteWaypoint> waypoints = new List<RouteWaypoint>();
            foreach (var schedule in trip.Schedules)
            {
                waypoints.Add(new RouteWaypoint(schedule.Place.Name, new GeoPoint(schedule.Place.lat, schedule.Place.lng)));
            }
            routeProvider.CalculateRoute(waypoints);
        }
    }
}