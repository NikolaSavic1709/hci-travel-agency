using DevExpress.Xpf.Core.Native;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace travelAgency.Dialogs
{
    /// <summary>
    /// Interaction logic for CreatePlaceDialog.xaml
    /// </summary>
    public partial class CreatePlaceDialog : Window
    {
        public CreatePlaceDialog()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OutlinedComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string selected= comboBox.SelectedItem.ToString();
            if (selected.Contains("Accomodation"))
                AmenitiesFragment.Visibility = Visibility.Visible;
            else
                AmenitiesFragment.Visibility = Visibility.Hidden;
        }
    }
    public class VisibilityToGridHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibility = (Visibility)value;
            return visibility == Visibility.Visible ? new GridLength(200) : new GridLength(0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
