using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using travelAgency.model;
using travelAgency.repository;

namespace travelAgency.view;

/// <summary>
/// Interaction logic for PlaceEdit.xaml
/// </summary>
public partial class PlaceEdit : Window
{
    private Place place;
    public PlaceEdit(Place? place)
    {
        InitializeComponent();
        if(place!=null)
        {
            this.place = place;
            NameTxtBox.Text=place.Name;
            DescriptionTxtBox.Text=place.Description;
            LocationTxtBox.Text = place.Location;
        }
    }

    private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text

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
        this.place.Name = NameTxtBox.Text;
        this.place.Description = DescriptionTxtBox.Text;
        this.place.Location = LocationTxtBox.Text;
        Close();
    }
}
