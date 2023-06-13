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
using travelAgency.model;

namespace travelAgency.view
{
    /// <summary>
    /// Interaction logic for ClientPlaceDetails.xaml
    /// </summary>
    public partial class ClientPlaceDetails : Window
    {
        public ClientPlaceDetails(Place place)
        {
            InitializeComponent();
            DataContext = this;
            if (place is Stay)
                Main.Content = new ClientStayEatPage(place, true);
            else
                Main.Content = new ClientStayEatPage(place, false);
            Help.HelpProvider.SetHelpKey((DependencyObject)Main, "client-index");
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(System.Windows.Application.Current.Windows[0]);
            if (focusedControl is DependencyObject)
            {
                string str = Help.HelpProvider.GetHelpKey((DependencyObject)Main);
                Help.HelpProvider.ShowHelp(str, this);
            }
        }
    }
}