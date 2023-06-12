using DevExpress.Xpf.Core;
using DevExpress.Xpf.Map;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
using travelAgency.Help;
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
        public PlaceRepository placeRepository;
        public ArrangementRepository arrangementRepository;

        public AgentHome(TravelAgencyContext dbContext)
        {
            InitializeComponent();
            HomeButton.IsClicked = "True";

            this.dbContext = dbContext;
            tripRepository = new TripRepository(dbContext);
            placeRepository = new PlaceRepository(dbContext);
            arrangementRepository = new ArrangementRepository(dbContext);
            Main.Content = new HomePage(tripRepository, placeRepository);
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
            switch (button.Name)
            {
                case "HomeButton":
                    Main.Content = new HomePage(tripRepository, placeRepository);
                    break;

                case "PlacesButton":
                    Main.Content = new PlacesPage();
                    break;

                case "StayEatButton":
                    Main.Content = new StayEatPage();
                    break;

                case "ReportButton":
                    Main.Content = new ReportPage(tripRepository, arrangementRepository);
                    break;

                case "HistoryButton":
                    Main.Content = new HistoryPage(dbContext, null);
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
        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(System.Windows.Application.Current.Windows[0]);
            if (focusedControl is DependencyObject)
            {
                string str = Help.HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                Help.HelpProvider.ShowHelp(str, this);
            }
        }


        private void ToHome_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DeselectNavbarButtons();
            HomeButton.IsClicked = "True";
            Main.Content = new HomePage(tripRepository, placeRepository);
        }

        private void ToPlaces_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DeselectNavbarButtons();
            PlacesButton.IsClicked = "True";
            Main.Content = new PlacesPage();
        }

        private void ToStayEat_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DeselectNavbarButtons();
            StayEatButton.IsClicked = "True";
            Main.Content = new StayEatPage();
        }

        private void ToReport_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DeselectNavbarButtons();
            ReportButton.IsClicked = "True";
            Main.Content = new ReportPage(tripRepository, arrangementRepository);
        }

        private void ToHistory_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DeselectNavbarButtons();
            HistoryButton.IsClicked = "True";
            Main.Content = new HistoryPage(dbContext, null);
        }

        private void Logout_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var logoutDialog = new LogoutDialog();

            logoutDialog.DialogResultEvent += DialogWindow_DialogResultEvent;

            logoutDialog.ShowDialog();
        }
    }
}