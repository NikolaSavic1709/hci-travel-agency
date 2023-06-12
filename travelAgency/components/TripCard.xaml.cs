using DevExpress.Xpf.Core.DragDrop.Native;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using travelAgency.Dialogs;
using travelAgency.model;

namespace travelAgency.components
{
    /// <summary>
    /// Interaction logic for TripCard.xaml
    /// </summary>
    public partial class TripCard : UserControl
    {
        public static readonly DependencyProperty TripProperty =
    DependencyProperty.Register("Trip", typeof(Trip), typeof(TripCard), new PropertyMetadata(null));

        public Trip Trip
        {
            get { return (Trip)GetValue(TripProperty); }
            set
            {
                SetValue(TripProperty, value);
                if (((Trip)value).Schedules.Count != 0)
                {
                    Route = ((Trip)value).Schedules[0].Place.Name.ToString() + " - " + ((Trip)value).Schedules.Last().Place.Name.ToString();
                    Trip t = ((Trip)value);
                    TripName = t.Name;
                }
            }
        }

        public string TripName { get; set; }
        public string Route { get; set; }

        public TripCard()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event EventHandler<ToTripEventArgs> ToTripClicked;

        public event EventHandler<ToTripEventArgs> TripDelete;

        private void OpenButton_click(object sender, RoutedEventArgs e)
        {
            ToTripClicked?.Invoke(this, new ToTripEventArgs(Trip));
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            TripDelete?.Invoke(this, new ToTripEventArgs(Trip));
        }
    }
}