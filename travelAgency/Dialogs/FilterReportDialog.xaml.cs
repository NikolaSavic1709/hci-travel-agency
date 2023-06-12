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
    /// Interaction logic for FilterReportDialog.xaml
    /// </summary>
    public partial class FilterReportDialog : Window
    {

        List<ReportCard> reportCards;
        public FilterReportDialog(List<ReportCard> reportCardss)
        {
            this.reportCards = new List<ReportCard>();
            foreach (ReportCard card in reportCardss)
            {
                reportCards.Add(card);
            }
            InitializeComponent();
            Loaded += Window_Loaded;
        }

        public event EventHandler<ReportCardEventArgs> DialogResultEvent;
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            //DialogResultEvent?.Invoke(this, new ArrangementCardEventArgs(arrangementCards));
            Close();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        public void Update()
        {
            int maxnp, minnp;
            double maxprice, minprice;
            maxnp = (int)personsSlider.UpperValue;
            minnp = (int)personsSlider.LowerValue;
            maxprice = (int)priceSlider.UpperValue;
            minprice = (int)priceSlider.LowerValue;

            reportCards.RemoveAll(card => card.TotalCount > maxnp || card.TotalCount < minnp);
            reportCards.RemoveAll(card => card.TotalPrice > maxprice || card.TotalPrice < minprice);
            DialogResultEvent?.Invoke(this, new ReportCardEventArgs(reportCards));
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int maxnp = 0;
            int minnp = 100000000;
            double maxprice = 0;
            double minprice = 100000000000000;
            foreach (ReportCard a in reportCards)
            {
                int np = a.TotalCount;
                double price = a.TotalPrice;
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

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Update();
        }

        private void Quit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }
    }
}
