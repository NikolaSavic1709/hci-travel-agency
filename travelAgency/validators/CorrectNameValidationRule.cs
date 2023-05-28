using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace travelAgency.validators
{
    public class CorrectNameValidationRule : ValidationRule
    {
        public string? Field { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string text)
            {

                if (!text.All(char.IsLetter))
                {
                    return new ValidationResult(false, $"Invalid {Field}, only letters allowed.");
                }
            }

            return ValidationResult.ValidResult;
        }
    }
}
