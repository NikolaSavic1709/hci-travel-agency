using DevExpress.Xpf.Core;
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
            Main.Content = new HomePage();

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
                    Main.Content = new HistoryPage();
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
