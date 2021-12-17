using System;

namespace TagCloudUsageSample.Validators
{
    public class StringValidatorAttribute : Attribute
    {
        private readonly Func<string, bool> validationFunc;
        private readonly string outMessage;

        protected StringValidatorAttribute(Func<string, bool> validationFunc, string message)
        {
            this.validationFunc = validationFunc;
            outMessage = message;
        }

        public bool Validate(string value, out string message)
        {
            var result = validationFunc(value);
            message = result ? "" : $"{outMessage}: {value}";
            return result;
        }
    }
}