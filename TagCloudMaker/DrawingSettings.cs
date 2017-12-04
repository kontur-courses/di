using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloud
{
    public class DrawingSettings
    {
        public readonly Color Background;
        public readonly Color BrushColor;
        public readonly FontFamily FontFamily;
        public readonly Size ImageSize;
        public readonly ImageFormat ImageFormat;

        public DrawingSettings(Color background, Color brushColor, FontFamily fontFamily, Size imageSize, ImageFormat imageFormat)
        {
            Background = background;
            BrushColor = brushColor;
            FontFamily = fontFamily;
            ImageSize = imageSize;
            ImageFormat = imageFormat;
        }
    }
}