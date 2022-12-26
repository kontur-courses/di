using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TagsCloudContainer.ConsoleApp
{
    public static class ColorHexValidator
    {
        private static readonly Regex colorRegex = new Regex("#[0-9a-fA-F]{8}");
        private const string colorFormat = "#FFFFFFFF";

        public static ValidationResult ValidateColor(string argbColor)
        {
            if (colorRegex.IsMatch(argbColor))
                return ValidationResult.Success;
            return new ValidationResult($"Value does not match the format '{colorFormat}'");
        }
    }
}