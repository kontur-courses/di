using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TagsCloudContainer.GUI.OptionsValidationRules
{
    public class IntegerRangeRule : ValidationRule
    {
        private int minValue;
        public int MinValue
        {
            get => minValue;
            set
            {
                if (value <= 0 || value > maxValue)
                    throw new ArgumentException(nameof(MinValue));

                minValue = value;
            }
        }

        private int maxValue;
        public int MaxValue
        {
            get => maxValue;
            set
            {
                if (value <= 0 || value < minValue)
                    throw new ArgumentException(nameof(MaxValue));

                maxValue = value;
            }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!int.TryParse(value.ToString(), out var integerValue))
                return new ValidationResult(false, $"'{value}' is not a number");

            if (integerValue < MinValue || integerValue > MaxValue)
                return new ValidationResult(false, $"Value should be in [{MinValue}; {MaxValue}]");

            return ValidationResult.ValidResult;
        }
    }
}