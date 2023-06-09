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

namespace travelAgency.components
{
    /// <summary>
    /// Interaction logic for TripCard.xaml
    /// </summary>
    public partial class TripCard : UserControl
    {

        public static readonly DependencyProperty TripNameProperty =
            DependencyProperty.Register("TripName", typeof(string), typeof(TripCard), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty RouteProperty =
            DependencyProperty.Register("Route", typeof(string), typeof(TripCard), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(TripCard), new PropertyMetadata(string.Empty));


        public string TripName
        {
            get { return (string)GetValue(TripNameProperty); }
            set { SetValue(TripNameProperty, value); }
        }

        public string Route
        {
            get { return (string)GetValue(RouteProperty); }
            set { SetValue(RouteProperty, value); }
        }

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public TripCard()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void OpenButton_click(object sender, RoutedEventArgs e)
        {

        }
    }
}
