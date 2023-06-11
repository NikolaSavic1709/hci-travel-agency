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
using travelAgency.Dialogs;
using travelAgency.model;

namespace travelAgency.components;

/// <summary>
/// Interaction logic for PlaceCard.xaml
/// </summary>
public partial class PlaceCard : UserControl
{
    public PlaceCard()
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
        }
    }
    public string Route { get; set; }

    public event EventHandler<ToPlaceEventArgs> ToPlaceClicked;
    private void OpenButton_click(object sender, RoutedEventArgs e)
    {
        ToPlaceClicked?.Invoke(this, new ToPlaceEventArgs(Place));
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {

    }
}
