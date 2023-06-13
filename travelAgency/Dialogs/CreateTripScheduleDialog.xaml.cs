using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using travelAgency.model;
using travelAgency.repository;
using travelAgency.ViewModel;

namespace travelAgency.Dialogs
{
    /// <summary>
    /// Interaction logic for CreateTripScheduleDialog.xaml
    /// </summary>
    public partial class CreateTripScheduleDialog : Window
    {
        List<Place> places;
        int currentIndexListBox = -1;
        Trip Trip { get; set; }
        TripRepository tripRepository;
        private PlaceRepository placeRepository;
        TripSchedule TripSchedule { get; set; }
        public string PlaceName { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Time { get; set; }
        public CreateTripScheduleDialog(Trip trip, TripRepository tripRepository, PlaceRepository placeRepository, TripSchedule? tripScheduleForEdit)
        {
            InitializeComponent();
            Trip = trip;
            this.tripRepository = tripRepository;
            this.placeRepository = placeRepository;
            places = placeRepository.GetAll();

            DataContext = this;


            if (tripScheduleForEdit != null)
            {
                TripSchedule = tripScheduleForEdit;
                PlaceTextBox.IsEnabled = false;
                PlaceName = TripSchedule.Place.Name;
                AutocompleteListBox.Visibility = Visibility.Hidden;
                Date = TripSchedule.DateTime;
                Time = TripSchedule.DateTime;
            }
            else
            {
                PlaceName = "";
                PlaceTextBox.Focus();
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = PlaceTextBox.Text;
            if (text.Length > 2 && PlaceTextBox.IsEnabled)
            {
                AutocompleteListBox.Visibility = Visibility.Visible;
                AutocompleteListBox.ItemsSource = places.Where(p => p.Name.ToLower().Contains(text.ToLower()));
                AutocompleteListBox.SelectedIndex = -1;
            }
            else
                AutocompleteListBox.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TextBox_OnFocusLost(object sender, RoutedEventArgs e)
        {
            AutocompleteListBox.Visibility = Visibility.Hidden;
            currentIndexListBox = -1;
            TextBox textBox = (TextBox)sender;
            BindingExpression bindingExpr = textBox.GetBindingExpression(TextBox.TextProperty);

            // Manually trigger the validation
            bindingExpr.UpdateSource();
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
            if (e.Key == Key.Return)
            {
                e.Handled = true;
                if (currentIndexListBox != -1)
                {
                    AutocompleteListBox.SelectedIndex = currentIndexListBox;
                }
            }
        }
        public event EventHandler<DialogResultEventArgs> DialogResultEvent;
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Place? selectedPlace = places.Find(p => p.Name == PlaceTextBox.Text);

            if (selectedPlace != null)
            {
                DateTime? date = DatePicker.SelectedDate.Value;
                DateTime? time = TimePicker.SelectedTime.Value;

                if (TripSchedule!=null)
                {
                    TripSchedule.DateTime = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, time.Value.Hour, time.Value.Minute, 0);
                    Trip.Schedules.Remove(TripSchedule);
                    Trip.Schedules.Add(TripSchedule);
                }
                else
                {
                    TripSchedule tripSchedule = new TripSchedule();
                    tripSchedule.Place = selectedPlace;
                    
                    tripSchedule.DateTime = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, time.Value.Hour, time.Value.Minute, 0);
                    Trip.Schedules.Add(tripSchedule);
                }
                DialogResultEvent?.Invoke(this, new DialogResultEventArgs(true));
                tripRepository.Save();
                Close();
            }
            else
            {
                if (Snackbar.MessageQueue is { } messageQueue)
                {
                    var message = "Place doesn't exist";
                    messageQueue.Enqueue(message);
                }
            }
        }
        private void DatePicker_LostFocus(object sender, RoutedEventArgs e)
        {
            DatePicker datePicker = (DatePicker)sender;
            BindingExpression bindingExpr = datePicker.GetBindingExpression(DatePicker.SelectedDateProperty);

            // Manually trigger the validation
            bindingExpr.UpdateSource();
        }
        private void TimePicker_LostFocus(object sender, RoutedEventArgs e)
        {
            TimePicker timePicker = (TimePicker)sender;
            BindingExpression bindingExpr = timePicker.GetBindingExpression(TimePicker.SelectedTimeProperty);

            // Manually trigger the validation
            bindingExpr.UpdateSource();
        }
    }
}