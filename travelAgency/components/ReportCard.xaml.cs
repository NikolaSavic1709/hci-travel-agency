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
using travelAgency.view;

namespace travelAgency.components
{
    /// <summary>
    /// Interaction logic for ReportCard.xaml
    /// </summary>
    public partial class ReportCard : UserControl
    {
        public Arrangement Arrangement
        {
            get { return (Arrangement)GetValue(ArrangementProperty); }
            set
            {
                SetValue(ArrangementProperty, value);
                Arrangement a = ((Arrangement)value);
                Trip = a.Trip;
                TotalPrice = a.Trip.Price;
                TotalCount = a.NumberOfPersons;
            }
        }
        public Trip Trip { get; set; }

        public double TotalPrice { get; set; }

        public int TotalCount { get; set; }
        public ReportCard()
        {
            InitializeComponent();
            DataContext = this;
        }
        public static readonly DependencyProperty ArrangementProperty = DependencyProperty.Register("Arrangement", typeof(Arrangement), typeof(ReportCard), new PropertyMetadata(null));



    }
}
