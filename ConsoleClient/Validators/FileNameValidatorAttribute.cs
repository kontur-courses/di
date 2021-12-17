using System.IO;
using System.Linq;

namespace TagCloudUsageSample.Validators
{
    public class FileNameValidatorAttribute : StringValidatorAttribute
    {
        public FileNameValidatorAttribute(string message) 
            : base(s => !s.Any(x => Path.GetInvalidFileNameChars().Contains(x)), message)
        {
        }
    }
}