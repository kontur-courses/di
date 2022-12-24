using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TagsCloudContainer.GUI.OptionsValidationRules
{
    public class ColorRule : ValidationRule
    {
        private static readonly Regex colorRegex = new Regex("#[0-9a-fA-F]{8}");
        private const string colorFormat = "#FFFFFFFF";

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var colorStr = (string)value;
            if (colorRegex.IsMatch(colorStr))
                return ValidationResult.ValidResult;
            return new ValidationResult(false, $"Value does not match the format '{colorFormat}'");
        }
    }
}