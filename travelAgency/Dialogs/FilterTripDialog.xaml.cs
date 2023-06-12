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
using travelAgency.components;

namespace travelAgency.Dialogs
{
    /// <summary>
    /// Interaction logic for FilterTripDialog.xaml
    /// </summary>
    public partial class FilterTripDialog : Window
    {

        List<TripCard> tripCards;
        public FilterTripDialog(List<TripCard> tripCardss)
        {
            this.tripCards = new List<TripCard>();
            foreach (TripCard card in tripCardss)
            {
                tripCards.Add(card);
            }
            InitializeComponent();
            Loaded += Window_Loaded;
        }

        public event EventHandler<TripCardEventArgs> DialogResultEvent;
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
           // DialogResultEvent?.Invoke(this, new TripCardEventArgs(tripCards));
            Close();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            double maxprice, minprice;            
            maxprice = (int)priceSlider.UpperValue;
            minprice = (int)priceSlider.LowerValue;
            
            tripCards.RemoveAll(card => card.Trip.Price > maxprice || card.Trip.Price < minprice);
            DialogResultEvent?.Invoke(this, new TripCardEventArgs(tripCards));
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            double maxprice = 0;
            double minprice = 100000000000000;
            foreach (TripCard a in tripCards)
            {
                double price = a.Trip.Price;
                maxprice = Math.Max(price, maxprice);
                minprice = Math.Min(price, minprice);
            }
            priceSlider.Minimum = minprice;
            priceSlider.Maximum = maxprice;
            priceSlider.LowerValue = minprice;
            priceSlider.UpperValue = maxprice;
        }

    }
}
