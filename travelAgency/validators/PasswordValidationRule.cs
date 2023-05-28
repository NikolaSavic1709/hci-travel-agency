 using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace travelAgency.validators
{
    public class PasswordValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is SecureString password)
            {
                if (IsValidPassword(password))
                {
                    return ValidationResult.ValidResult;
                }
                else
                {
                    return new ValidationResult(false, "Password must have a min of 6 characters, at least 1 number, 1 special character and 1 uppercase letter.");
                }
            }

            return new ValidationResult(false, "Invalid input.");
        }

        private bool IsValidPassword(SecureString password)
        {
            string plainPassword = ConvertToPlainString(password);
            if (plainPassword.Length < 6)
            {
                return false;
            }

            if (!plainPassword.Any(char.IsDigit))
            {
                return false;
            }

            string specialCharPattern = @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]";

            if (!Regex.IsMatch(plainPassword, specialCharPattern))
            {
                return false;
            }

            if (!plainPassword.Any(char.IsUpper))
            {
                return false;
            }

            return true;
        }

        private string ConvertToPlainString(SecureString secureString)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return System.Runtime.InteropServices.Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
