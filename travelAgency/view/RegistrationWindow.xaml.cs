using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
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
            PhoneNumber = "";
            Error = "";
        }
        public string Error { get; set; }

        public string FirstName { get; set; }
        public bool IsFirstNameInvalid { get; set; }

        public string Surname { get; set; }
        public bool IsSurnameInvalid { get; set; }

        public string Email { get; set; }
        public bool IsEmailInvalid { get; set; }

        public string PhoneNumber { get; set; }
        public bool IsPhoneNumberInvalid { get; set; }

    //    public SecureString Password { get; set; }
        public bool IsPasswordInvalid { get; set; }
        public string PasswordInvalidError { get; set; }

        //   public SecureString ConfirmPassword { get; set; }
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
                    PasswordInvalidError = "Password must have a min of 6 characters, at least 1 number, 1 special character and 1 uppercase letter";
                    throw new ArgumentException("Password must have a min of 6 characters, at least 1 number, 1 special character and 1 uppercase letter");
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
                    ConfirmPasswordInvalidError = "Password must have a min of 6 characters, at least 1 number, 1 special character and 1 uppercase letter";
                    throw new ArgumentException("Password must have a min of 6 characters, at least 1 number, 1 special character and 1 uppercase letter");
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
            Error = "";

            if (string.IsNullOrWhiteSpace((FirstName ?? "").ToString()) || string.IsNullOrWhiteSpace((Surname ?? "").ToString()) ||
                string.IsNullOrWhiteSpace((Email ?? "").ToString()) || string.IsNullOrWhiteSpace((PhoneNumber ?? "").ToString()) ||
                string.IsNullOrWhiteSpace((_password1Validated ?? "").ToString()) || string.IsNullOrWhiteSpace((_password2Validated ?? "").ToString()))
            {
                Error = "All fields are required";
                return;
            }

            if (IsFirstNameInvalid)
            {
                Error = "Invalid Name, only letters allowed";
                return;
            }

            if (IsSurnameInvalid)
            {
                Error = "Invalid Surname, only letters allowed";
                return;
            }

            if (IsEmailInvalid)
            {
                Error = "Please enter a valid email address";
                return;
            }

            if (IsPhoneNumberInvalid)
            {
                Error = "Please enter a valid phone number for Serbia";
                return;
            }

            if (IsPasswordInvalid)
            {
                Error = PasswordInvalidError;
                return;
            }

            if (IsConfirmPasswordInvalid)
            {
                Error = ConfirmPasswordInvalidError;
                return;
            }


            var newUser = new User(Name,Email,_password1Validated,Surname,PhoneNumber);
            userRepository.Add(newUser);

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
            PhoneNumberTxtBox.Text = "";
            EmailTxtBox.Text = "";
            PasswordTxtBox.Password = "";
            ConfirmPasswordTxtBox.Password = "";
            Error = "";
        }

        private void To_Login(object sender, MouseButtonEventArgs e)
        {
            //LoginWindow loginWindow = new LoginWindow();
            //loginWindow.Show();
            //Close();
        }

        


    }

}
