using MahApps.Metro.Controls;
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
using travelAgency.repository;

namespace travelAgency.Dialogs
{
    /// <summary>
    /// Interaction logic for FilterArrangementDialog.xaml
    /// </summary>
    public partial class FilterArrangementDialog : Window
    {
        List<ArrangementCard> arrangementCards;
        public FilterArrangementDialog(List<ArrangementCard> arrangementCardss)
        {
            this.arrangementCards = new List<ArrangementCard>();
            foreach(ArrangementCard card in arrangementCardss)
            {
                arrangementCards.Add(card);
            }
            InitializeComponent();
            Loaded += Window_Loaded;
        }

        public event EventHandler<ArrangementCardEventArgs> DialogResultEvent;
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            //DialogResultEvent?.Invoke(this, new ArrangementCardEventArgs(arrangementCards));
            Close();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            int maxnp, minnp;
            double maxprice,minprice;
            maxnp = (int)personsSlider.UpperValue;
            minnp = (int)personsSlider.LowerValue;
            maxprice = (int)priceSlider.UpperValue;
            minprice = (int)priceSlider.LowerValue;
            if (PurchaseCheckBox.IsChecked == false)
            {
                arrangementCards.RemoveAll(card => !card.Arrangement.IsReservation);
            }
            if (ReservationCheckBox.IsChecked == false)
            {
                arrangementCards.RemoveAll(card => card.Arrangement.IsReservation);
            }
            arrangementCards.RemoveAll(card => card.Arrangement.NumberOfPersons>maxnp || card.Arrangement.NumberOfPersons < minnp);
            arrangementCards.RemoveAll(card => card.Arrangement.Price > maxprice || card.Arrangement.Price < minprice);
            DialogResultEvent?.Invoke(this, new ArrangementCardEventArgs(arrangementCards));
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int maxnp = 0;
            int minnp = 100000000;
            double maxprice = 0;
            double minprice = 100000000000000;
            foreach (ArrangementCard a in arrangementCards)
            {
                int np = a.Arrangement.NumberOfPersons;
                double price = a.Arrangement.Price;
                maxnp = Math.Max(np, maxnp);
                minnp = Math.Min(np, minnp);
                minprice = Math.Min(price, minprice);
                maxprice = Math.Max(price, maxprice);
            }
            personsSlider.Minimum = minnp;
            personsSlider.Maximum = maxnp;
            personsSlider.LowerValue = minnp;
            personsSlider.UpperValue = maxnp;
            priceSlider.Minimum = minprice;
            priceSlider.Maximum = maxprice;
            priceSlider.LowerValue = minprice;
            priceSlider.UpperValue = maxprice;
        }

    }
}
