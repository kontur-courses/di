using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TagsCloudContainer.GUI.OptionsValidationRules
{
    public class FloatRangeRule : ValidationRule
    {
        private float minValue;
        public float MinValue
        {
            get => minValue;
            set
            {
                if (value <= 0 || value > maxValue)
                    throw new ArgumentException(nameof(MinValue));

                minValue = value;
            }
        }

        private float maxValue;
        public float MaxValue
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
            if (!float.TryParse(value.ToString(), out var floatValue))
                return new ValidationResult(false, $"'{value}' is not a number");

            if (floatValue < MinValue || floatValue > MaxValue)
                return new ValidationResult(false, $"Value should be in [{MinValue}; {MaxValue}]");

            return ValidationResult.ValidResult;
        }
    }
}