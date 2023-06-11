using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace travelAgency.validators
{
    public class EmailValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string email)
            {
                if (IsValidEmail(email))
                {
                    return ValidationResult.ValidResult;
                }
                else
                {
                    return new ValidationResult(false, "Please enter a valid email address.");
                }
            }

            return new ValidationResult(false, "Invalid input.");
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            return Regex.IsMatch(email, pattern);
        }
    }
}