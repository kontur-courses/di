using System.Drawing;

namespace TagCloudCreator
{
    public class Configuration
    {
        public string InputFile { get; set; }
        public string OutputFile { get; set; }
        public string StopWordsFile { get; set; }
        public Color BackgroundColor { get; set; }
        public Size ImageSize { get; set; }

        public Configuration(
            string inputFile,
            string outputFile,
            string stopWordsFile,
            Color backgroundColor,
            Size imageSize)
        {
            InputFile = inputFile;
            OutputFile = outputFile;
            StopWordsFile = stopWordsFile;
            BackgroundColor = backgroundColor;
            ImageSize = imageSize;
        }
    }
}