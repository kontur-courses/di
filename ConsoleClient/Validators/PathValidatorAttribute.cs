using System.IO;

namespace TagCloudUsageSample
{
    public class PathValidatorAttribute : StringValidatorAttribute
    {
        public PathValidatorAttribute(string message) 
            : base(Directory.Exists, message)
        {
        }
    }
}