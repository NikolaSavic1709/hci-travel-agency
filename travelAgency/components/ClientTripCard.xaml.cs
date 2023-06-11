using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using travelAgency.Dialogs;
using travelAgency.model;

namespace travelAgency.components;

/// <summary>
/// Interaction logic for ClientTripCard.xaml
/// </summary>
public partial class ClientTripCard : UserControl
{
    public ClientTripCard()
    {
        InitializeComponent();
        DataContext = this;
    }

    public static readonly DependencyProperty TripProperty =
  DependencyProperty.Register("Trip", typeof(Trip), typeof(TripCard), new PropertyMetadata(null));

    public Trip Trip
    {
        get { return (Trip)GetValue(TripProperty); }
        set
        {
            SetValue(TripProperty, value);
            Route = ((Trip)value).Schedules[0].Place.Name.ToString() + " - " + ((Trip)value).Schedules.Last().Place.Name.ToString();
        }
    }

    public string Route { get; set; }

    public event EventHandler<ToTripEventArgs> ToTripClicked;

    private void OpenButton_click(object sender, RoutedEventArgs e)
    {
        ToTripClicked?.Invoke(this, new ToTripEventArgs(Trip));
    }
}