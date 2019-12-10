using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer.Core
{
    public class Parameters
    {
        public string InputFilePath { get; }

        public string OutputFilePath { get; }

        public List<Color> Colors { get; }

        public Font Font { get; }

        public Size ImageSize { get; }
        public ImageFormat ImageFormat { get; }

        public Parameters(string inputFilePath, string outputFilePath, List<Color> colors, Font font, Size imageSize,
            ImageFormat imageFormat)
        {
            InputFilePath = inputFilePath;
            OutputFilePath = outputFilePath;
            Colors = colors;
            Font = font;
            ImageSize = imageSize;
            ImageFormat = imageFormat;
        }
    }
}