using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace travelAgency.validators
{
    public class NonZeroCountValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is int count && count == 0)
            {
                return new ValidationResult(false, "Count must be greater than zero.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
