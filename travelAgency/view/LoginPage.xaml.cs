using MaterialDesignThemes.Wpf;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using travelAgency.model;
using travelAgency.repository;

namespace travelAgency.view
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public TravelAgencyContext dbContext;
        public UserRepository userRepository;
        private Window window;

        public LoginPage(Window window)
        {
            this.window = window;
            InitializeComponent();
            DataContext = this;

            dbContext = new TravelAgencyContext();
            userRepository = new UserRepository(dbContext);

            Email = "";
            Error = "";
        }

        public string Error { get; set; }

        public string Email { get; set; }
        public bool IsEmailInvalid { get; set; }

        public bool IsPasswordInvalid { get; set; }
        public string PasswordInvalidError { get; set; }

        private string? _password1Validated = "";

        public string? Password1Validated
        {
            get => _password1Validated;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    PasswordInvalidError = "Password cannot be empty";
                    throw new ArgumentException("Password cannot be empty");
                }

                string specialCharPattern = @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]";

                if (value.Length < 6 || !value.Any(char.IsDigit) || !Regex.IsMatch(value, specialCharPattern) || !value.Any(char.IsUpper))
                {
                    PasswordInvalidError = "Password requirements: 6+ characters, 1 number, 1 special character, 1 uppercase";
                    throw new ArgumentException("Password requirements: 6+ characters, 1 number, 1 special character, 1 uppercase");
                }

                _password1Validated = value;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            BindingExpression bindingExpr = textBox.GetBindingExpression(TextBox.TextProperty);

            // Manually trigger the validation
            bindingExpr.UpdateSource();
        }

        private void PasswordBox_Changed(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox)sender;
            BindingExpression bindingExpr = passwordBox.GetBindingExpression(PasswordBoxAssist.PasswordProperty);

            // Manually trigger the validation
            bindingExpr.UpdateSource();
        }

        public event EventHandler ToSignUpClicked;

        private void To_SignUp(object sender, MouseButtonEventArgs e)
        {
            ToSignUpClicked?.Invoke(this, EventArgs.Empty);
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            ActivateProgress();
            await Task.Delay(500);

            Error = "";
            err.Text = "";

            if (string.IsNullOrWhiteSpace((Email ?? "").ToString()) || string.IsNullOrWhiteSpace((_password1Validated ?? "").ToString()))
            {
                Error = "All fields are required";
                err.Text = "All fields are required";
                DeactivateProgress();
                return;
            }

            if (IsEmailInvalid)
            {
                Error = "Please enter a valid email address";
                err.Text = "Please enter a valid email address";
                DeactivateProgress();
                return;
            }

            if (IsPasswordInvalid)
            {
                Error = PasswordInvalidError;
                err.Text = PasswordInvalidError;
                DeactivateProgress();
                return;
            }

            User loggedUser = userRepository.GetByEmail(Email);
            if (loggedUser == null || loggedUser.Password != _password1Validated)
            {
                Error = "Invalid login credentials. Please try again";
                err.Text = "Invalid login credentials. Please try again";

                SnackbarOk.MessageQueue?.Enqueue("Invalid login credentials",
                "OK",
               (arg) =>
               {
                   // Action button clicked
               },
               null,
               false,
               true,
               TimeSpan.FromSeconds(3));
            }
            else
            {
                Window agentHome = new AgentHome();
                agentHome.Show();
                this.window.Close();

                EmailTxtBox.Text = "";
                PasswordTxtBox.Password = "";
                Error = "";
            }
            await Task.Delay(500);
            DeactivateProgress();
        }

        private void DeactivateProgress()
        {
            LoginBtn.IsEnabled = true;
            LoginBtn.Margin = new Thickness(80, 0, 80, 0);
            LoginBtn.Background = new BrushConverter().ConvertFromString("#009882") as Brush;
            LoginBtn.Cursor = Cursors.Hand;
            ProgresBar.Visibility = Visibility.Collapsed;
        }

        private void ActivateProgress()
        {
            LoginBtn.IsEnabled = false;
            LoginBtn.Margin = new Thickness(80, 0, 10, 0);
            LoginBtn.Background = new BrushConverter().ConvertFromString("#609882") as Brush;
            LoginBtn.Cursor = Cursors.Arrow;
            ProgresBar.Visibility = Visibility.Visible;
        }
    }
}