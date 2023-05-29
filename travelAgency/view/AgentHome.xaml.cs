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
using System.Windows.Shapes;
using travelAgency.components;
using travelAgency.controls;
using travelAgency.model;
using travelAgency.repository;

namespace travelAgency.view
{
    /// <summary>
    /// Interaction logic for AgentHome.xaml
    /// </summary>
    public partial class AgentHome : Window
    {
        public TravelAgencyContext dbContext;
        public TripRepository tripRepository;
        public AgentHome()
        {

            InitializeComponent();

            dbContext = new TravelAgencyContext();
            tripRepository = new TripRepository(dbContext);

            List<Trip> trips = tripRepository.GetAll();
            foreach(Trip t in trips)
            {
                TripCard tripCard = new TripCard
                {
                    Margin = new Thickness(10),
                    TripName = t.Name,
                    Route = t.Price.ToString(),
                    Description = t.Description
                };
                cards.Children.Add(tripCard);
            }

            TripCard tripCard1 = new TripCard
            {
                Margin = new Thickness(10),
                TripName = "Tura zapadna Srbija",
                Route = "Šabac - Bajina Bašta",
                Description = "Tura je veoma zaniljiva i duga jer Savic i drugari imaju sta da ponude i njihovom kraju"
            };

            TripCard tripCard2 = new TripCard
            {
                Margin = new Thickness(10),
                TripName = "Planinski maratoni",
                Route = "Raška - Pančićev vrh",
                Description = "Tura je veoma zaniljiva i duga jer Savic i drugari imaju sta da ponude i njihovom kraju"
            };

            TripCard tripCard3 = new TripCard
            {
                Margin = new Thickness(10),
                TripName = "Tura južna Srbija",
                Route = "Vranje - Đavolja Varoš",
                Description = "Ubedljiva najbolja tura u nasoj ponudi"
            };

            cards.Children.Add(tripCard1);
            cards.Children.Add(tripCard2);
            cards.Children.Add(tripCard3);
        }

        private void Search_OnKeyDown(object sender, KeyEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (e.Key == Key.Enter)
                SearchButton.Command.Execute(textBox.Text);
        }
    }
}
