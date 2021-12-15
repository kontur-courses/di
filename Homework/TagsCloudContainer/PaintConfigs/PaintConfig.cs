using System.Drawing;

namespace TagsCloudContainer.PaintConfigs
{
    public class PaintConfig : IPaintConfig
    {
        public IColorScheme Color { get; }
        public string FontName { get; }
        public int FontSize { get; }
        public Size ImageSize { get; }

        public PaintConfig(IColorScheme color, 
            string fontName, int fontSize, Size imageSize)
        {
            Color = color;
            FontName = fontName;
            FontSize = fontSize;
            ImageSize = imageSize;
        }
    }
}
