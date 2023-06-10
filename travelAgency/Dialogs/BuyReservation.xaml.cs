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
using System.Windows.Shapes;

namespace travelAgency.view;

/// <summary>
/// Interaction logic for BuyReservation.xaml
/// </summary>
public partial class BuyReservation : Window
{
    private string RESERVATION = "Reservation";
    private string PURCHASE = "Purchase";
    private string RESERVE = "Reserve";
    private string BUY = "BUY";
    private int BuyCount = 1;
    public string Count { get; set; }
    public string Text { get; set; }
    public string TitleText { get; set; }
    public string Action { get; set; }
    public BuyReservation()
    {
        bool Reservation = true;
        InitializeComponent();
        Count = BuyCount.ToString();
        if (Reservation)
        {
            Text = RESERVATION;
            TitleText = RESERVATION;
            Action = RESERVE;
        }
        else
        {
            TitleText = PURCHASE;
            Action = BUY;
            Text = PURCHASE;
        }
        Text = Text + " for:";
        this.DataContext = this;
    }

    private void MinusClick(object sender, RoutedEventArgs e)
    {
        BuyCount--;
        if (BuyCount <= 1)
        {
            BuyCount = 1;
        }
        Count = BuyCount.ToString();
        CountLabel.Content = Count;
    }

    private void PlusClick(object sender, RoutedEventArgs e)
    {
        BuyCount += 1;
        Count = BuyCount.ToString();
        CountLabel.Content = Count;
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        (new Confirmation(true)).Show();
        this.Close();
    }
}
