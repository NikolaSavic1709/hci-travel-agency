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

namespace travelAgency.view
{
    /// <summary>
    /// Interaction logic for Confirmation.xaml
    /// </summary>
    public partial class Confirmation : Window
    {
        private string RESERVATION = "Reservation";
        private string PURCHASE = "Purchase";
        public string TitleText { get; set; }
        public string Text { get; set; }
        public Confirmation(bool Reservation)
        {
            InitializeComponent();

            if (Reservation)
            {
                TitleText = RESERVATION;
                Text = RESERVATION;
            }
            else
            {
                TitleText = PURCHASE;
                Text = PURCHASE;
            }
            Text = Text + " successful";
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    internal class DC
    {
        public string Title { get; set; }
        public string Text { get; set; }

        public DC(string Title, string Text)
        {
            Text = Text;
            Title = Title;
        }
    }
}
