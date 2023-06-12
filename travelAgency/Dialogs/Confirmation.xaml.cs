using System.Windows;

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

        private void Ok_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
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