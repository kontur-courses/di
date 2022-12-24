using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TagsCloudContainer.GUI.OptionsValidationRules
{
    public class InputWordFilenameRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var filename = (string)value;

            if (string.IsNullOrEmpty(filename))
                return new ValidationResult(false, "Filename is empty");

            if (File.Exists(filename))
                return ValidationResult.ValidResult;
            return new ValidationResult(false, "Path is incorrect");
        }
    }
}