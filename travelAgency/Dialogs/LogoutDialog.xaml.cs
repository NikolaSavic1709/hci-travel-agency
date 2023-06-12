using System;
using System.Windows;

namespace travelAgency.Dialogs
{
    /// <summary>
    /// Interaction logic for LogoutDialog.xaml
    /// </summary>
    public partial class LogoutDialog : Window
    {
        public event EventHandler<DialogResultEventArgs> DialogResultEvent;

        public LogoutDialog()
        {
            InitializeComponent();
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            DialogResultEvent?.Invoke(this, new DialogResultEventArgs(true));
            Close();
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            DialogResultEvent?.Invoke(this, new DialogResultEventArgs(false));
            Close();
        }

        private void Yes_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            DialogResultEvent?.Invoke(this, new DialogResultEventArgs(true));
            Close();
        }

        private void No_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            DialogResultEvent?.Invoke(this, new DialogResultEventArgs(false));
            Close();
        }
    }
}