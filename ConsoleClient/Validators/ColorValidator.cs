using System.Linq;
using ResultProject;

namespace TagCloudUsageSample.Validators
{
    public class ColorValidator : StringValidatorAttribute
    {
        public ColorValidator(string message) 
            : base(ValidateColor, message)
        {
        }

        private static Result<bool> ValidateColor(string color)
        {
            if (color is null) return true.AsResult();
            return color.AsResult()
                .Then(x => x.Split(' '))
                .Then(x => x.Length == 3 && x.All(cByte => byte.TryParse(cByte, out _)));
        }
    }
}