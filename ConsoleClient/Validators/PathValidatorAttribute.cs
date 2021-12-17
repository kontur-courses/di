using System.IO;

namespace TagCloudUsageSample.Validators
{
    public class PathValidatorAttribute : StringValidatorAttribute
    {
        public PathValidatorAttribute(string message) 
            : base(Directory.Exists, message)
        {
        }
    }
}