using System.Linq;
using ResultProject;

namespace TagCloudUsageSample.Validators
{
    public class ColorValidator : StringValidatorAttribute
    {
        public ColorValidator(string message) 
            : base(ThrowIfInvalid, message)
        {
        }

        private static bool ValidateColor(string color)
        {
            if (color is null) return true;
            var splitted = color.Split(' ');
            return splitted.Length == 3 && splitted.All(cByte => byte.TryParse(cByte, out _));
        }
        
        private static Result<bool> ThrowIfInvalid(string color)
        {
            if (color is null) return Result.Ok(true);
            return Result.Ok(color.Split(' '))
                .Then(x => x.Length == 3 && x.All(cByte => byte.TryParse(cByte, out _)));
            // var splitted = color.Split(' ');
            // return splitted.Length == 3 && splitted.All(cByte => byte.TryParse(cByte, out _));
        }
    }
}