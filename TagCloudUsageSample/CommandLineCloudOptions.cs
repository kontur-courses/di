using System.IO;
using System.Linq;
using CommandLine;

namespace TagCloudUsageSample
{
    public class CommandLineCloudOptions
    {
        public bool Validate(out string errorValidationMessage)
        {
            var propertiesToCheck = GetType().GetProperties().Where(x => x.GetCustomAttributes(true).OfType<RangeValidatorAttribute>().Any());
           

            foreach (var propertyInfo in propertiesToCheck)
            {
                var validator = propertyInfo.GetCustomAttributes(true).OfType<RangeValidatorAttribute>().First();
                if (!validator.Validate((int) propertyInfo.GetValue(this), out errorValidationMessage))
                    return false;
            }
            
            propertiesToCheck = GetType().GetProperties().Where(x => x.GetCustomAttributes(true).OfType<StringValidatorAttribute>().Any());

            foreach (var propertyInfo in propertiesToCheck)
            {
                var validator = propertyInfo.GetCustomAttributes(true).OfType<StringValidatorAttribute>().First();
                if (!validator.Validate((string) propertyInfo.GetValue(this), out errorValidationMessage))
                    return false;
            }
            
            errorValidationMessage = "";
            return true;
        }

        public string GetFullFilenameByNumber(int number)
            => SavePath.TrimEnd(Path.DirectorySeparatorChar) +
               Path.DirectorySeparatorChar +
               FileName + 
               (CloudCount == 1 ? "" : $"({number})") +
               ".jpg";

        [RangeValidatorAttribute(1, 100, nameof(CloudCount))]
        [Option('c', "count", Default = 1, HelpText = "Set required tag cloud count.")]
        public int CloudCount { get; private set; }
        
        [Option('o', "openFirst", Default = false, HelpText = "Open first created file")]
        public bool OpenFirst { get; set; }
        
        [RangeValidatorAttribute(1, 10000, nameof(RectangleCount))]
        [Option("rectangleCount", Default = 100, HelpText = "Set required rectangles count.")]
        public int RectangleCount { get; private set; }

        [PathValidatorAttribute("unknown directory")]
        [Option('p', "path", Default = "..\\..\\CloudSamples", HelpText = "Set path to save tag clouds.")]
        public string SavePath{ get; private set; }

        [FileNameValidatorAttribute("invalid file name")]
        [Option('n', "name", Required = true, HelpText = "Set name to save tag clouds.")]
        public string FileName { get; private set; }

        [RangeValidatorAttribute(1, 500, nameof(SizeCoefficient))]
        [Option('s', "size", Default = 100, HelpText = "Set rectangle size coefficient.")]
        public int SizeCoefficient { get; private set; }

        [RangeValidatorAttribute(1, 250, nameof(MinimumRectHeight))]
        [Option('m', "minimumRectHeight", Default = 2, HelpText = "Set minimum rectangle height.")]
        public int MinimumRectHeight{ get; private set; }
    }
}