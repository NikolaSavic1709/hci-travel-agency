using DevExpress.Internal.WinApi.Windows.UI.Notifications;
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
using travelAgency.model;

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
    }
}
