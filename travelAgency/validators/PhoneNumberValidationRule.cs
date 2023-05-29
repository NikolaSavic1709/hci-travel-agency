using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace travelAgency.validators
{
    public class PhoneNumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string phoneNumber)
            {
                if (IsValidPhoneNumber(phoneNumber))
                {
                    return ValidationResult.ValidResult;
                }
                else
                {
                    return new ValidationResult(false, "Please enter a valid phone number for Serbia.");
                }
            }

            return new ValidationResult(false, "Invalid input.");
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // for Serbia
            string pattern = @"^\+381\d{8,9}$";

            return Regex.IsMatch(phoneNumber, pattern);
        }
    }
}
