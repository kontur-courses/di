using System.Drawing;

namespace TagsCloudApp.App
{
    public class InitialSettings : IInitialSettings
    {
        public string InputFilePath { get; }
        public string OutputFilePath { get; }
        public Size ImageSize { get; }

        public Font WordsFont { get; }

        public Color WordsColor { get; }

        public InitialSettings(string inputFile, string outputFile, Size imageSize, Color _wordsColor,
                               Font _wordsFont)
        {
            InputFilePath = inputFile;
            OutputFilePath = outputFile;
            ImageSize = imageSize;
            WordsFont = _wordsFont;
            WordsColor = _wordsColor;
        }
    }
}
