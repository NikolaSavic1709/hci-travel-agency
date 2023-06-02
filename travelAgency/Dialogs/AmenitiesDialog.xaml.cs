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
using travelAgency.ViewModel;

namespace travelAgency.Dialogs
{
    /// <summary>
    /// Interaction logic for AmenitiesDialog.xaml
    /// </summary>
    public partial class AmenitiesDialog : Window
    {
        public AmenitiesDialog()
        {
            InitializeComponent();
            DataContext = new AmenitiesDialogViewModel();
        }

        private void RemoveAllBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddAllBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
