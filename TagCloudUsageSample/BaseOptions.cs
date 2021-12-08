using System.Linq;

namespace TagCloudUsageSample
{
    public abstract class BaseOptions
    {
        private bool ValidateRanges(out string errorValidationMessage)
        {
            var propertiesToCheck = GetType().GetProperties().Where(x => x.GetCustomAttributes(true).OfType<RangeValidatorAttribute>().Any());
           
            foreach (var propertyInfo in propertiesToCheck)
            {
                var validator = propertyInfo.GetCustomAttributes(true).OfType<RangeValidatorAttribute>().First();
                if (!validator.Validate((int) propertyInfo.GetValue(this), out errorValidationMessage))
                    return false;
            }
            
            errorValidationMessage = "";
            return true;
        }

        private bool ValidateStrings(out string errorValidationMessage)
        {
            var propertiesToCheck = GetType().GetProperties().Where(x => x.GetCustomAttributes(true).OfType<StringValidatorAttribute>().Any());

            foreach (var propertyInfo in propertiesToCheck)
            {
                var validator = propertyInfo.GetCustomAttributes(true).OfType<StringValidatorAttribute>().First();
                if (!validator.Validate((string) propertyInfo.GetValue(this), out errorValidationMessage))
                    return false;
            }
            
            errorValidationMessage = "";
            return true;
        }

        public bool Validate(out string errorValidationMessage)
        {
            return ValidateRanges(out errorValidationMessage) && ValidateStrings(out errorValidationMessage);
        }
    }
}