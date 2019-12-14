using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.UI
{
    public class InitialSettings : IInitialSettings
    {
        public string InputFilePath { get; set; }
        public string OutputFilePath { get; set; }
        public Size ImageSize { get; set; }

        public InitialSettings()
        {
        }

        public InitialSettings(string inputFile, string outputFile, Size imageSize)
        {
            InputFilePath = inputFile;
            OutputFilePath = outputFile;
            ImageSize = imageSize;
        }

        public object Clone()
        {
            var newSettings = new InitialSettings();
            foreach (var propertyInfo in GetType().GetProperties())
            {
                propertyInfo.SetValue(newSettings, propertyInfo.GetValue(this));
            }

            return newSettings;
        }
    }
}
