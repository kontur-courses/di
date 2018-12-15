using System.Drawing;
using TagsCloudContainer.UI;
using TagsCloudContainer.Visualisation.Coloring;

namespace TagsCloudContainer.Visualisation
{
    public class ImageSettings
    {
        public FontFamily FontFamily { get; }
        public Size ImageSize { get; }
        public string OutputPath { get; }
        public Size LetterSize { get; }
        public Color TextColor { get; }
        public bool AutoSize { get; }


        public ImageSettings
            (FontFamily fontFamily, Size imageSize, Size letterSize, string outputPath, Color textColor, bool autoSize)
        {
            FontFamily = fontFamily;
            ImageSize = imageSize;
            OutputPath = outputPath;
            LetterSize = letterSize;
            TextColor = textColor;
            AutoSize = autoSize;
        }
    }
}