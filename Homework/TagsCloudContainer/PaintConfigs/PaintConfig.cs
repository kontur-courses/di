using System.Drawing;

namespace TagsCloudContainer.PaintConfigs
{
    public class PaintConfig : IPaintConfig
    {
        public Brush Color { get; }
        public string FontName { get; }
        public int FontSize { get; }
        public Size ImageSize { get; }

        public PaintConfig(Brush color, 
            string fontName, int fontSize, Size imageSize)
        {
            Color = color;
            FontName = fontName;
            FontSize = fontSize;
            ImageSize = imageSize;
        }
    }
}
