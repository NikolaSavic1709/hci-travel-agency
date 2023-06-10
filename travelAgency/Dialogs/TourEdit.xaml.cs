using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using travelAgency.model;
using travelAgency.repository;

namespace travelAgency.view;

/// <summary>
/// Interaction logic for Window1.xaml
/// </summary>
public partial class TourEdit : Window
{

    private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
    TripRepository tripRepository;
    private Trip trip;
    private static bool IsTextAllowed(string text)
    {
        return !_regex.IsMatch(text);
    }
    private void TextBoxPasting(object sender, DataObjectPastingEventArgs e)
    {
        if (e.DataObject.GetDataPresent(typeof(String)))
        {
            String text = (String)e.DataObject.GetData(typeof(String));
            if (!IsTextAllowed(text))
            {
                e.CancelCommand();
            }
        }
        else
        {
            e.CancelCommand();
        }
    }

    public TourEdit(Trip? trip, TripRepository tripRepository)
    {
        InitializeComponent();
        this.tripRepository = tripRepository;
        
        if(trip!=null)
        {
            this.trip = trip;
            NameTxtBox.Text=trip.Name;
            DescriptionTxtBox.Text = trip.Description;
            PriceTxtBox.Text = trip.Price + " IZM";
        }
    }

    private void Price_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !IsTextAllowed(e.Text);
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        this.trip.Name=NameTxtBox.Text;
        this.trip.Description=DescriptionTxtBox.Text;
        this.trip.Price=Convert.ToDouble(PriceTxtBox.Text) ;
        tripRepository.Save();
        Close();
    }
}
