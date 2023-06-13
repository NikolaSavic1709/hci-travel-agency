using System.Globalization;
using System.Windows.Controls;

namespace travelAgency.validators
{
    public class ComboBoxSelectionValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return new ValidationResult(false, "Item selection is required.");
            }

            return ValidationResult.ValidResult;
        }

    }
}
