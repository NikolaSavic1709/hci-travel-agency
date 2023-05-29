using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;
using travelAgency.controls;
using travelAgency.Dialogs;
using travelAgency.model;
using travelAgency.ViewModel;

namespace travelAgency
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        string ImageDirectory { get; set; }
        string[] ImageNames { get; set; }
        int CurrentImageIndex { get; set; }
        public Window1()
        {

            InitializeComponent();
            DataContext = new ImageSliderViewModel();
            ImageDirectory = "C:\\semestar6\\HCI\\vezbe\\projekat\\hci-travel-agency\\travelAgency\\Resources\\Images";
            ImageNames = Directory.GetFiles(ImageDirectory);
            CurrentImageIndex = 0;
            var viewModel = DataContext as ImageSliderViewModel;
            if (viewModel != null)
            {
                viewModel.FrontImageSource = ImageNames[CurrentImageIndex];
                viewModel.BackImageSource = ImageNames[CurrentImageIndex];
                Trip trip = new Trip();
                Place place1 = new Place();
                place1.Name = "Sabac";
                Place place2 = new Place();
                place2.Name = "Novi Sad";

                TripSchedule ts1 = new TripSchedule();
                ts1.Place = place1;
                TripSchedule ts2 = new TripSchedule();
                ts2.Place = place2;
                trip.Schedules = new List<TripSchedule> { ts1, ts2 };
                viewModel.Trip = trip;
            }
            HomeButton.IsClicked = "True";
        }
        private void SlideLeft(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as ImageSliderViewModel;
            if (viewModel != null)
            {
                CurrentImageIndex = (CurrentImageIndex - 1 + ImageNames.Length) % ImageNames.Length;
                viewModel.BackImageSource = ImageNames[CurrentImageIndex];
                DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.1) };
                timer.Tick += (s, args) =>
                {
                    ImageFlipper.IsFlipped = !ImageFlipper.IsFlipped;
                    viewModel.FrontImageSource = viewModel.BackImageSource;
                    timer.Stop();
                };
                timer.Start();
            }
        }

        private void SlideRight(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as ImageSliderViewModel;
            if (viewModel != null)
            {
                CurrentImageIndex = (CurrentImageIndex + 1) % ImageNames.Length;
                viewModel.BackImageSource = ImageNames[CurrentImageIndex];
                DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.1) };
                timer.Tick += (s, args) =>
                {
                    ImageFlipper.IsFlipped = !ImageFlipper.IsFlipped;
                    viewModel.FrontImageSource = viewModel.BackImageSource;
                    timer.Stop();
                };
                timer.Start();
            }
        }

        private void DecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            int number = int.Parse(YearTb.Text);
            if (number > 1) YearTb.Text = (number - 1).ToString();
        }

        private void IncreaseButton_Click(object sender, RoutedEventArgs e)
        {
            int number = int.Parse(YearTb.Text);
            if (number < 2023) YearTb.Text = (number + 1).ToString();
        }

        private void NumericTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {

            foreach (var c in e.Text)
            {
                if (!char.IsDigit(c))
                {
                    e.Handled = true;
                    return;
                }
            }
            int number = int.Parse(((TextBox)sender).Text + e.Text);
            if (number > 2023)
            {
                e.Handled = true; return;
            }
        }

        private void SelectMonth_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            CalendarLabel label = (CalendarLabel)sender;
            SelectMonthLabels(label);
            tb1.Text = label.Content + " " + YearTb.Text;
        }
        private void SelectMonthLabels(CalendarLabel selectedLabel)
        {
            CalendarLabel label;
            if (Calendar != null)
            {
                foreach (var child in GetChildren(Calendar))
                {
                    label = (CalendarLabel)child;
                    if (label == selectedLabel)
                        label.IsSelected = "True";
                    else
                        label.IsSelected = "False";
                }
            }
        }

        private IEnumerable<DependencyObject> GetChildren(DependencyObject parent)
        {
            if (parent == null)
                yield break;

            int childCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                yield return child;

            }
        }

        private void NavbarButton_Click(object sender, RoutedEventArgs e)
        {
            NavbarButton button = (NavbarButton)sender;
            ClickNavbarButton(button);
        }
        private void ClickNavbarButton(NavbarButton clickedButton)
        {
            NavbarButton button;
            if (NavbarButtons != null)
            {
                foreach (var child in GetChildren(NavbarButtons))
                {
                    button = (NavbarButton)child;
                    if (button == clickedButton)
                        button.IsClicked = "True";
                    else
                        button.IsClicked = "False";
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateTripDialog window = new CreateTripDialog();
            window.Show();
        }
    }
}
