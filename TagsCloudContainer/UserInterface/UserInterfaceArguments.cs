using System.Collections.Generic;

namespace TagsCloudContainer.UserInterface
{
    public class UserInterfaceArguments
    {
        public string InputFilePath { get; set; }

        public string OutputFilePath { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string Font { get; set; }

        public List<string> Colors { get; set; }

        public string ImageFormat { get; set; }

        public UserInterfaceArguments(string inputFilePath, string outputFilePath, int width, int height, string font,
            List<string> colors, string imageFormat)
        {
            InputFilePath = inputFilePath;
            OutputFilePath = outputFilePath;
            Width = width;
            Height = height;
            Font = font;
            Colors = colors;
            ImageFormat = imageFormat;
        }

        public UserInterfaceArguments()
        {

        }
    }
}