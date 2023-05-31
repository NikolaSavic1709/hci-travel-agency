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
using travelAgency.ViewModel;

namespace travelAgency.Dialogs
{
    /// <summary>
    /// Interaction logic for CreateTripDialog.xaml
    /// </summary>
    public partial class CreateTripDialog : Window
    {
        List<Place> allPlaces;
        CreateTripViewModel ViewModel { get; set; }
        int currentIndexListBox=-1;

        public CreateTripDialog()
        {
            InitializeComponent();
            DataContext = new CreateTripViewModel();
            var viewModel = DataContext as CreateTripViewModel;
            if (viewModel != null)
            {
                ViewModel = viewModel;
                Trip trip = new Trip();
                viewModel.Trip=trip;
            }
            Place place1 = new Place();
            place1.Name = "Sabac - Srbija";
            Place place2 = new Place();
            place2.Name = "Novi Sad - Srbija";
            Place place3 = new Place();
            place3.Name = "Beograd";
            allPlaces=new List<Place> { place1, place2, place3 };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = PlaceTextBox.Text;
            if (text.Length > 2)
            {
                AutocompleteListBox.Visibility = Visibility.Visible;
                AutocompleteListBox.ItemsSource = allPlaces.Where(p => p.Name.ToLower().Contains(text.ToLower()));
                AutocompleteListBox.SelectedIndex = -1;
            }
            else
                AutocompleteListBox.Visibility = Visibility.Hidden;

        }
        private void TextBox_OnFocusLost(object sender, RoutedEventArgs e)
        {
            AutocompleteListBox.Visibility = Visibility.Hidden;
            currentIndexListBox = -1;
        }

        private void AutocompleteListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Place? place = AutocompleteListBox.SelectedItem as Place;
            if (place != null)
            {
                PlaceTextBox.Text = place.Name;
                AutocompleteListBox.Visibility = Visibility.Hidden;
                currentIndexListBox = -1;
            }
        }
        private void window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
            AutocompleteListBox.Visibility = Visibility.Hidden;
            currentIndexListBox = -1;
        }

        private void AddPlace_Click(object sender, RoutedEventArgs e)
        {
            Place? selectedPlace = allPlaces.Find(p => p.Name == PlaceTextBox.Text);
            
            if (selectedPlace != null)
            {
                TripSchedule tripSchedule = new TripSchedule();
                tripSchedule.Place = selectedPlace;
                DateTime? date=DatePicker.SelectedDate.Value;
                DateTime? time = TimePicker.SelectedTime.Value;
                
                tripSchedule.DateTime=new DateTime(date.Value.Year,date.Value.Month,date.Value.Day,time.Value.Hour,time.Value.Minute,0);
                ViewModel.Trip.Schedules.Add(tripSchedule);
                
            }
            else
            {
                //handle
            }
        }

        private void RemovePlace_Click(object sender, RoutedEventArgs e)
        {
            Button removeButton = (Button)sender;
            TripSchedule tripSchedule = (TripSchedule)removeButton.DataContext;
            ViewModel.Trip.Schedules.Remove(tripSchedule);
        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                e.Handled = true;
                if (AutocompleteListBox.Items.Count > 0)
                {
                    currentIndexListBox = (currentIndexListBox + 1) % AutocompleteListBox.Items.Count;
                    Place? place = AutocompleteListBox.Items[currentIndexListBox] as Place;
                    if (place != null)
                    {
                        PlaceTextBox.TextChanged -= TextBox_TextChanged;
                        PlaceTextBox.Text = place.Name;
                        PlaceTextBox.TextChanged += TextBox_TextChanged;
                    }
                }
            }
            if (e.Key == Key.Up)
            {
                e.Handled = true;
                if (AutocompleteListBox.Items.Count > 0)
                {
                    if (currentIndexListBox == -1) currentIndexListBox = 0;
                    currentIndexListBox = (currentIndexListBox - 1 + AutocompleteListBox.Items.Count) % AutocompleteListBox.Items.Count;
                    Place? place = AutocompleteListBox.Items[currentIndexListBox] as Place;
                    if (place != null)
                    {
                        PlaceTextBox.TextChanged -= TextBox_TextChanged;
                        PlaceTextBox.Text = place.Name;
                        PlaceTextBox.TextChanged += TextBox_TextChanged;
                    }
                }
            }
            if (e.Key==Key.Return)
            {
                e.Handled = true;
                if (currentIndexListBox != -1)
                {
                    AutocompleteListBox.SelectedIndex= currentIndexListBox;
                }
            }
        }
    }
}
