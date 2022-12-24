using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TagsCloudContainer.GUI.OptionsValidationRules
{
    public class FontFamilyRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var fontFamily = (string)value;
            if (FontFamily.Families.Any(ff => ff.Name.ToLower() == fontFamily))
                return ValidationResult.ValidResult;
            return new ValidationResult(false, $"Font family '{fontFamily}' does not exist");
        }
    }
}