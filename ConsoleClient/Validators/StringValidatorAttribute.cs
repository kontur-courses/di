using System;
using ResultProject;

namespace TagCloudUsageSample.Validators
{
    public class StringValidatorAttribute : Attribute
    {
        private readonly Func<string, Result<bool>> validationFunc;
        private readonly string outMessage;

        protected StringValidatorAttribute(Func<string, Result<bool>> validationFunc, string message)
        {
            this.validationFunc = validationFunc;
            outMessage = message;
        }

        public Result<bool> Validate(string value)
        {
            return validationFunc(value).ThenFailIf(x => !x, $"{outMessage}: {value}");
            // var result = validationFunc(value);
            // message = result ? "" : $"{outMessage}: {value}";
            // return result;
        }
    }
}