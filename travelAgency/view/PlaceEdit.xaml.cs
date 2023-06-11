using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace travelAgency.view;

/// <summary>
/// Interaction logic for PlaceEdit.xaml
/// </summary>
public partial class PlaceEdit : Window
{
    public PlaceEdit()
    {
        InitializeComponent();
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
}