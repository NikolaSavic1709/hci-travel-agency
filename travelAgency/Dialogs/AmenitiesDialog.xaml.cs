using System.Windows;
using travelAgency.ViewModel;

namespace travelAgency.Dialogs
{
    /// <summary>
    /// Interaction logic for AmenitiesDialog.xaml
    /// </summary>
    public partial class AmenitiesDialog : Window
    {
        private AmenitiesDialogViewModel amenitiesDialogViewModel;

        public AmenitiesDialog()
        {
            InitializeComponent();
            amenitiesDialogViewModel = new AmenitiesDialogViewModel();
            DataContext = amenitiesDialogViewModel;
        }

        private void RemoveAllBtn_Click(object sender, RoutedEventArgs e)
        {
            amenitiesDialogViewModel.RemoveAll();
        }

        private void AddAllBtn_Click(object sender, RoutedEventArgs e)
        {
            amenitiesDialogViewModel.AddAll();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}