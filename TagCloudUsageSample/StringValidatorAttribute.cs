using System;
using System.IO;
using System.Linq;

namespace TagCloudUsageSample
{
    public class FileNameValidatorAttribute : StringValidatorAttribute
    {
        public FileNameValidatorAttribute(string message) 
            : base(s => !s.Any(x => Path.GetInvalidFileNameChars().Contains(x)), message)
        {
        }
    }
    
    public class PathValidatorAttribute : StringValidatorAttribute
    {
        public PathValidatorAttribute(string message) 
            : base(Directory.Exists, message)
        {
        }
    }
    
    public class StringValidatorAttribute : Attribute
    {
        private readonly Func<string, bool> validationFunc;
        private readonly string outMessage;

        public StringValidatorAttribute(Func<string, bool> validationFunc, string message)
        {
            this.validationFunc = validationFunc;
            outMessage = message;
        }

        public bool Validate(string value, out string message)
        {
            var result = validationFunc(value);
            message = result ? "" : outMessage;
            return result;
        }
    }
}