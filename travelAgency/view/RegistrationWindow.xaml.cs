using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
using travelAgency.repository;

namespace travelAgency.view
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {

        public TravelAgencyContext dbContext;
        public UserRepository userRepository;
        public RegistrationWindow()
        {
            InitializeComponent();
            DataContext = this;

            dbContext = new TravelAgencyContext();
            userRepository = new UserRepository(dbContext);

            FirstName = "";
            Surname = "";
            Email = "";
            //PhoneNumber = "";
            Error = "";
        }
        public string Error { get; set; }

        public string FirstName { get; set; }
        public bool IsFirstNameInvalid { get; set; }

        public string Surname { get; set; }
        public bool IsSurnameInvalid { get; set; }

        public string Email { get; set; }
        public bool IsEmailInvalid { get; set; }

        //public string PhoneNumber { get; set; }
        //public bool IsPhoneNumberInvalid { get; set; }

        public bool IsPasswordInvalid { get; set; }
        public string PasswordInvalidError { get; set; }

        public bool IsConfirmPasswordInvalid { get; set; }
        public string ConfirmPasswordInvalidError { get; set; }


        private string? _password1Validated = "";
        private string? _password2Validated = "";
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

        public string? Password2Validated
        {
            get => _password2Validated;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    ConfirmPasswordInvalidError = "Password cannot be empty";
                    throw new ArgumentException("Password cannot be empty");
                }

                string specialCharPattern = @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]";

                if (value.Length < 6 || !value.Any(char.IsDigit) || !Regex.IsMatch(value, specialCharPattern) || !value.Any(char.IsUpper))
                {
                    ConfirmPasswordInvalidError = "Password requirements: 6+ characters, 1 number, 1 special character, 1 uppercase";
                    throw new ArgumentException("Password requirements: 6+ characters, 1 number, 1 special character, 1 uppercase");
                }

                if (!string.Equals(_password1Validated, value))
                {
                    ConfirmPasswordInvalidError = "Passwords do not match";
                    throw new ArgumentException("Passwords do not match");
                }
                   

                _password2Validated =  value;
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            ActivateProgress();

            Error = "";
            err.Text = "";

            if (string.IsNullOrWhiteSpace((FirstName ?? "").ToString()) || string.IsNullOrWhiteSpace((Surname ?? "").ToString()) ||
                string.IsNullOrWhiteSpace((Email ?? "").ToString()) || string.IsNullOrWhiteSpace((_password1Validated ?? "").ToString()) ||
                string.IsNullOrWhiteSpace((_password2Validated ?? "").ToString()))
            {
                Error = "All fields are required";
                err.Text = "All fields are required";
                return;
            }

            if (IsFirstNameInvalid)
            {
                Error = "Invalid Name, only letters allowed";
                err.Text = "Invalid Name, only letters allowed";
                return;
            }

            if (IsSurnameInvalid)
            {
                Error = "Invalid Surname, only letters allowed";
                err.Text = "Invalid Surname, only letters allowed";

                return;
            }

            if (IsEmailInvalid)
            {
                Error = "Please enter a valid email address";
                return;
            }

            //if (IsPhoneNumberInvalid)
            //{
            //    Error = "Please enter a valid phone number for Serbia";
            //    err.Text = "Please enter a valid phone number for Serbia";
                
            //    return;
            //}

            if (IsPasswordInvalid)
            {
                Error = PasswordInvalidError;
                err.Text = PasswordInvalidError;

                return;
            }

            if (IsConfirmPasswordInvalid)
            {
                Error = ConfirmPasswordInvalidError;
                err.Text = ConfirmPasswordInvalidError;
                return;
            }

            var newuser = new User(Name, Email, _password1Validated, Surname);
            userRepository.Add(newuser);

            SnackbarOk.MessageQueue?.Enqueue("You have successfully registered!",
                 "OK",
                (arg) =>
                {
                    // Action button clicked
                },
                null,
                false,
                true,
                TimeSpan.FromSeconds(3));

            NameTxtBox.Text = "";
            SurnameTxtBox.Text = "";
            //PhoneNumberTxtBox.Text = "";
            EmailTxtBox.Text = "";
            PasswordTxtBox.Password = "";
            ConfirmPasswordTxtBox.Password = "";
            Error = "";

            DeactivateProgress();
        }

        private void DeactivateProgress()
        {
            RegisterBtn.IsEnabled = false;
            RegisterBtn.Margin = new Thickness(80, 0, 80, 0);
            RegisterBtn.Background = new BrushConverter().ConvertFromString("#009882") as Brush;
            RegisterBtn.Cursor = Cursors.Hand;
            ProgresBar.Visibility = Visibility.Collapsed;
        }

        private void ActivateProgress()
        {
            RegisterBtn.IsEnabled = false;
            RegisterBtn.Margin = new Thickness(80, 0, 10, 0);
            RegisterBtn.Background = new BrushConverter().ConvertFromString("#609882") as Brush;
            RegisterBtn.Cursor = Cursors.Arrow;
            ProgresBar.Visibility = Visibility.Visible;
        }

        private void To_Login(object sender, MouseButtonEventArgs e)
        {
            //LoginWindow loginWindow = new LoginWindow();
            //loginWindow.Show();
            //Close();
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
    }

}
