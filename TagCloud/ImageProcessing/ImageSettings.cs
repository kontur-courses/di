using System.Drawing;
namespace TagCloud
{
    public class ImageSettings : IImageSettings
    {
        public Size Size { get; }

        public Color BackgroundColor { get; }

        public FontFamily FontFamily { get; }

        public int MinFontSize { get; }

        public int MaxFontSize { get; }

        public IWordColoring WordColoring { get; }

        public ImageSettings(Size size, Color backgroundColor, FontFamily fontFamily, int minFontSize, int maxFontSize, IWordColoring wordColoring)
        {
            Size = size;
            BackgroundColor = backgroundColor;
            FontFamily = fontFamily;
            MinFontSize = minFontSize;
            MaxFontSize = maxFontSize;
            WordColoring = wordColoring;
        }

    }
}
