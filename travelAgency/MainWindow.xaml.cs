using System.IO;
using System;
using System.Windows;
using travelAgency.ViewModel;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Collections.Generic;
using travelAgency.UImodels;

namespace travelAgency
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string ImageDirectory { get; set; }
        string[] ImageNames { get; set; }
        int CurrentImageIndex { get; set; }
        
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ImageSliderViewModel();
            ImageDirectory = "C:\\Nikola\\programiranje\\6_semestar\\covek_racunar\\hci-travel-agency\\travelAgency\\Resources\\Images";
            ImageNames = Directory.GetFiles(ImageDirectory);
            CurrentImageIndex= 0;
            var viewModel = DataContext as ImageSliderViewModel;
            if (viewModel != null)
            {
                viewModel.FrontImageSource = ImageNames[CurrentImageIndex];
                viewModel.BackImageSource = ImageNames[CurrentImageIndex];
            }
        }
        private void SlideLeft(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as ImageSliderViewModel;
            if (viewModel != null)
            {
                CurrentImageIndex = (CurrentImageIndex - 1+ ImageNames.Length) % ImageNames.Length;
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
            if (number <2023) YearTb.Text = (number + 1).ToString();
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
            int number = int.Parse(((TextBox)sender).Text+e.Text);
            if (number > 2023)
            {
                e.Handled = true; return;
            }
        }

        private void SelectMonth_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            CalendarLabel label = (CalendarLabel)sender;
            GetMonthLabels(label);
            tb1.Text = label.Content+" " + YearTb.Text;
        }
        private void GetMonthLabels(CalendarLabel selectedLabel)
        {
            CalendarLabel label;
            if (Calendar != null)
            {
                foreach (var child in GetChildren(Calendar))
                {
                    label = (CalendarLabel)child;
                    if (label == selectedLabel)
                        label.IsClicked = "True";
                    else
                        label.IsClicked = "False";
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

    }
}
