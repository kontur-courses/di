using System.IO;

namespace TagCloudUsageSample
{
    public class FileValidatorAttribute : StringValidatorAttribute
    {
        public FileValidatorAttribute(string message) 
            : base(File.Exists, message)
        {
        }
    }
}