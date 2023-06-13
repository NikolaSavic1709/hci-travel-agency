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
using System.Windows.Navigation;
using System.Windows.Shapes;
using travelAgency.model;

namespace travelAgency.components
{
    /// <summary>
    /// Interaction logic for ArrangementCard.xaml
    /// </summary>
    public partial class ArrangementCard : UserControl
    {
        public static readonly DependencyProperty ArrangementProperty =
        DependencyProperty.Register("Arrangement", typeof(Arrangement), typeof(ArrangementCard), new PropertyMetadata(null));

        public Arrangement Arrangement
        {
            get { return (Arrangement)GetValue(ArrangementProperty); }
            set
            {
                Arrangement a = ((Arrangement)value);
                SetValue(ArrangementProperty, value);
                if (a.IsReservation)
                {
                    IsReservation = "Reservation";
                }
                else
                {
                    IsReservation = "Purchase";
                }
                TotalPrice = (int)Math.Round((double)(a.NumberOfPersons * a.Trip.Price));
                Trip = a.Trip;
            }
        }
        public string IsReservation { get; set; }
        public Trip Trip { get; set; }
        public int TotalPrice { get; set; }
        public ArrangementCard()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
