using System;
using System.Windows;
using System.Windows.Controls;
using travelAgency.Dialogs;
using travelAgency.model;
using travelAgency.view;

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
   DependencyProperty.Register("StayEat", typeof(Place), typeof(AttractionCard), new PropertyMetadata(null));

    public Place StayEat
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
            Place p = ((Place)value);
            StayEatName = p.Name;
        }
    }
    public string StayEatName { get; set; }
    public string Route { get; set; }

    public event EventHandler<ToStayEatEventArgs> ToStayEatClicked;
    public event EventHandler<ToStayEatEventArgs> StayEatDelete;

    private void OpenButton_click(object sender, RoutedEventArgs e)
    {
        ToStayEatClicked?.Invoke(this, new ToStayEatEventArgs(StayEat));
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        StayEatDelete?.Invoke(this, new ToStayEatEventArgs(StayEat));
    }
}