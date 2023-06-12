using System;
using System.Windows;
using System.Windows.Controls;
using travelAgency.Dialogs;
using travelAgency.model;

namespace travelAgency.components;

/// <summary>
/// Interaction logic for AttractionCard.xaml
/// </summary>
public partial class AttractionCard : UserControl
{
    public AttractionCard()
    {
        InitializeComponent();
        DataContext = this;
    }

    public static readonly DependencyProperty AttractionProperty =
    DependencyProperty.Register("Attraction", typeof(Attraction), typeof(AttractionCard), new PropertyMetadata(null));

    public Attraction Attraction
    {
        get { return (Attraction)GetValue(AttractionProperty); }
        set
        {
            Attraction a = ((Attraction)value);
            SetValue(AttractionProperty, value);
            AttractionName = a.Name;
        }
    }
    public string AttractionName { get; set; }
    public string Route { get; set; }

    public event EventHandler<ToAttractionEventArgs> ToAttractionClicked;
    public event EventHandler<ToAttractionEventArgs> AttractionDelete;

    private void OpenButton_click(object sender, RoutedEventArgs e)
    {
        ToAttractionClicked?.Invoke(this, new ToAttractionEventArgs(Attraction));
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        AttractionDelete?.Invoke(this, new ToAttractionEventArgs(Attraction));
    }
}