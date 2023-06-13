using System.Windows;
using travelAgency.model;
using travelAgency.repository;

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

    public Arrangement Arrangement { get; set; }

    public TravelAgencyContext dbContext;
    public ArrangementRepository arrangementRepository;

    public BuyReservation(Arrangement arrangement, TravelAgencyContext dbContex)
    {
        dbContext = new TravelAgencyContext();
        this.arrangementRepository = new ArrangementRepository(dbContex);
        Arrangement = arrangement;
        bool Reservation = Arrangement.IsReservation;
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
        Text = Text + " for: " + Arrangement.Trip.Name;
        this.DataContext = this;
        Arrangement.Price = (double)(Arrangement.Trip.Price * BuyCount);
        Arrangement.NumberOfPersons = BuyCount;
        PriceLabel.Content = Arrangement.Price.ToString();
    }

    private void MinusClick(object sender, RoutedEventArgs e)
    {
        Minus();
    }

    private void PlusClick(object sender, RoutedEventArgs e)
    {
        Plus();
    }

    public void Minus()
    {
        BuyCount--;
        if (BuyCount <= 1)
        {
            BuyCount = 1;
        }
        Count = BuyCount.ToString();
        CountLabel.Content = Count;
        Arrangement.Price = (double)(Arrangement.Trip.Price * BuyCount);
        Arrangement.NumberOfPersons = BuyCount;
        PriceLabel.Content = Arrangement.Price.ToString();
    }

    public void Plus()
    {
        BuyCount += 1;
        Count = BuyCount.ToString();
        CountLabel.Content = Count;
        Arrangement.NumberOfPersons = BuyCount;
        Arrangement.Price = (double)(Arrangement.Trip.Price * BuyCount);

        PriceLabel.Content = Arrangement.Price.ToString();
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        Arrangement.DateTime = System.DateTime.Now;
        Arrangement.TripId = Arrangement.Trip.Id;
        Arrangement.UserId = Arrangement.User.Id;
        arrangementRepository.Add(Arrangement);
        (new Confirmation(true)).ShowDialog();
        this.Close();
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void Yes_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
    {
        Arrangement.DateTime = System.DateTime.Now;
        Arrangement.TripId = Arrangement.Trip.Id;
        Arrangement.UserId = Arrangement.User.Id;
        arrangementRepository.Add(Arrangement);
        (new Confirmation(true)).ShowDialog();
        this.Close();
    }

    private void No_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
    {
        this.Close();
    }

    private void Plus_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
    {
        Plus();
    }

    private void Minus_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
    {
        Minus();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}