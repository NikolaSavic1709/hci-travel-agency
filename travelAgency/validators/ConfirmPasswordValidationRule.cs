using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace travelAgency.validators
{
    public class ConfirmPasswordValidationRule: ValidationRule
    {


        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            PasswordBox ppasswordBox = Application.Current.Windows.OfType<travelAgency.view.RegistrationWindow>().FirstOrDefault()?.FindName("PasswordTxtBox") as PasswordBox;

            if (value is string confirmPassword && ppasswordBox is PasswordBox passwordBox)
            {
                string password = passwordBox.Password;

                if (string.Equals(password, confirmPassword))
                {
                    return ValidationResult.ValidResult;
                }
                else
                {
                    return new ValidationResult(false, "Passwords do not match.");
                }
            }

            return ValidationResult.ValidResult;
        }
    }
}
