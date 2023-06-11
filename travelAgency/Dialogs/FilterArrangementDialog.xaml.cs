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
using travelAgency.repository;

namespace travelAgency.Dialogs
{
    /// <summary>
    /// Interaction logic for FilterArrangementDialog.xaml
    /// </summary>
    public partial class FilterArrangementDialog : Window
    {
        public FilterArrangementDialog()
        {
            InitializeComponent();
        }

        public event EventHandler<DialogResultEventArgs> DialogResultEvent;
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResultEvent?.Invoke(this, new DialogResultEventArgs(false));
            Close();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {

            DialogResultEvent?.Invoke(this, new DialogResultEventArgs(true));
            Close();
        }
    }
}
