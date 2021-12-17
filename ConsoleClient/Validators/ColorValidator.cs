using System.Linq;

namespace TagCloudUsageSample.Validators
{
    public class ColorValidator : StringValidatorAttribute
    {
        public ColorValidator(string message) 
            : base(ValidateColor, message)
        {
        }

        private static bool ValidateColor(string color)
        {
            if (color is null) return true;
            var splitted = color.Split(' ');
            return splitted.Length == 3 && splitted.All(cByte => byte.TryParse(cByte, out _));
        }
    }
}