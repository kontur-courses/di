using System.IO;

namespace TagCloudUsageSample
{
    public class FileValidatorAttribute : StringValidatorAttribute
    {
        public FileValidatorAttribute(string message) 
            : base(s => s is null || File.Exists(s), message)
        {
        }
    }
}