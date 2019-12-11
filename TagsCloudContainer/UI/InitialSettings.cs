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
        public string InputFilePath { get; }
        public string OutputFilePath { get; }
        public Size ImageSize { get; }

        public InitialSettings(string inputFile, string outputFile, Size imageSize)
        {
            InputFilePath = inputFile;
            OutputFilePath = outputFile;
            ImageSize = imageSize;
        }
    }
}
