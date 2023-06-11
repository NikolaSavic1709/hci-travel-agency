using DevExpress.Xpf.Core;
using DevExpress.Xpf.Map;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using travelAgency.components;
using travelAgency.controls;
using travelAgency.Dialogs;
using travelAgency.model;
using travelAgency.repository;

namespace travelAgency.view
{
    /// <summary>
    /// Interaction logic for AgentHome.xaml
    /// </summary>
    public partial class AgentHome : Window
    {
        

        
        public AgentHome()
        {
            
            InitializeComponent();

            HomeButton.IsClicked = "True";
           // Main.Content = new HomePage();


            Stay stay = new Stay();
            List<Amenity> amenities = new List<Amenity>();
            Amenity amenity = new Amenity();
            amenity.amenity = AmenityEnum.AirCondition;

            Amenity amenity2 = new Amenity();
            amenity2.amenity = AmenityEnum.Bar;

            amenities.Add(amenity);
            amenities.Add(amenity2);
            stay.StayAmenities = amenities;

            stay.Name = "Blend Elphistone resort";
            stay.Description = "The hotel consists of the main building and 9 annex buildings, offering 271 accommodation units. Bars, restaurants and sports facilities are available to guests.For children there is a swimming pool, animations, mini club, playground.";
            stay.Location = "The hotel is located about 15 km from the center of Zlatibor and about 25 km from the top of Tornik.";

            StayRepository stayRepository = new StayRepository(new TravelAgencyContext());
            stayRepository.Add(stay);

            //Restaurant place = new Restaurant();
            //place.Name = "Blend Elphistone resort";
            //place.Description = "The hotel consists of the main building and 9 annex buildings, offering 271 accommodation units. Bars, restaurants and sports facilities are available to guests.For children there is a swimming pool, animations, mini club, playground.";
            //place.Location = "The hotel is located about 15 km from the center of Zlatibor and about 25 km from the top of Tornik.";

            Main.Content = new AdminStayEatDetailPage(stay,true);
        }

        

        private IEnumerable<DependencyObject> GetChildren(DependencyObject parent)
        {
            if (parent == null)
                yield break;

            int childCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                yield return child;

            }
        }

        private void NavbarButton_Click(object sender, RoutedEventArgs e)
        {
            NavbarButton button = (NavbarButton)sender;
            DeselectNavbarButtons();
            button.IsClicked = "True";
            switch(button.Name)
            {
                case "HomeButton":
                    Main.Content = new HomePage();
                    break;
                case "PlacesButton":
                    Main.Content = new PlacesPage();
                    break;
                case "StayEatButton":
                    Main.Content = new StayEatPage();
                    break;
                case "ReportButton":
                    Main.Content = new ReportPage();
                    break;
                case "HistoryButton":
                    Main.Content = new HistoryPage(null);
                    break;
                default:
                    break;
            }
        }
        private void DeselectNavbarButtons()
        {
            if (NavbarButtons != null)
            {
                foreach (var child in GetChildren(NavbarButtons))
                {
                    ((NavbarButton)child).IsClicked = "False";
                }
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var logoutDialog = new LogoutDialog();

            logoutDialog.DialogResultEvent += DialogWindow_DialogResultEvent;

            logoutDialog.ShowDialog();
        }

        private void DialogWindow_DialogResultEvent(object sender, DialogResultEventArgs e)
        {
            bool result = e.Result;
            if (result)
            {
                Window loginwindow = new RegistrationWindow();
                loginwindow.Show();
                this.Close();
            }
           
        }
    }
}
