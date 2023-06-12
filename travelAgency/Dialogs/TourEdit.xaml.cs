using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using travelAgency.Dialogs;
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
    public string Name1 { get; set; }
    public string Description { get; set; }
    public string Price { get; set; }
    public TourEdit(Trip? trip, TripRepository tripRepository)
    {
        InitializeComponent();
        this.tripRepository = tripRepository;
        DataContext = this;
        if (trip!=null)
        {
            this.trip = trip;
            Name1=trip.Name;
            Description = trip.Description;
            Price = trip.Price.ToString();
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
    public event EventHandler<DialogResultEventArgs> DialogResultEvent;
    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        Update();
    }

    public void Update()
    {
        this.trip.Name = NameTxtBox.Text;
        this.trip.Description = DescriptionTxtBox.Text;
        this.trip.Price = Convert.ToDouble(PriceTxtBox.Text);
        tripRepository.Save();
        DialogResultEvent?.Invoke(this, new DialogResultEventArgs(true));
        Close();

    }

    private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Update();
    }

    private void Quit_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Close();
    }


    private void TextBox_LostFocus(object sender, RoutedEventArgs e)
    {
        TextBox textBox = (TextBox)sender;
        BindingExpression bindingExpr = textBox.GetBindingExpression(TextBox.TextProperty);

        // Manually trigger the validation
        bindingExpr.UpdateSource();
    }
}
