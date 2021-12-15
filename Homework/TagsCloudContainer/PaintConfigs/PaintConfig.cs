using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer.PaintConfigs
{
    public class PaintConfig : IPaintConfig
    {
        public ImageFormat ImageFormat { get; }
        public IColorScheme Color { get; }
        public string FontName { get; }
        public int FontSize { get; }
        public Size ImageSize { get; }

        public PaintConfig(IColorScheme color, ImageFormat imageFormat,
            string fontName, int fontSize, Size imageSize)
        {
            ImageFormat = imageFormat;
            Color = color;
            FontName = fontName;
            FontSize = fontSize;
            ImageSize = imageSize;
        }
    }
}
