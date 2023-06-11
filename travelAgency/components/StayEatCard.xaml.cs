using System;
using System.Windows;
using System.Windows.Controls;
using travelAgency.Dialogs;
using travelAgency.model;

namespace travelAgency.components;

/// <summary>
/// Interaction logic for StayEatCard.xaml
/// </summary>
public partial class StayEatCard : UserControl
{
    public string Type { get; set; }

    public StayEatCard()
    {
        InitializeComponent();
        DataContext = this;
    }

    public static readonly DependencyProperty PlaceProperty =
   DependencyProperty.Register("Place", typeof(Place), typeof(PlaceCard), new PropertyMetadata(null));

    public Place Place
    {
        get { return (Place)GetValue(PlaceProperty); }
        set
        {
            SetValue(PlaceProperty, value);
            if (value is Stay)
            {
                Type = "Stay";
            }
            else
            {
                Type = "Restaurant";
            }
        }
    }

    public string Route { get; set; }

    public event EventHandler<ToStayEatEventArgs> ToStayEatClicked;

    private void OpenButton_click(object sender, RoutedEventArgs e)
    {
        ToStayEatClicked?.Invoke(this, new ToStayEatEventArgs(Place));
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
    }
}