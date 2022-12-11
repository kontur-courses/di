using System.Drawing;
using TagsCloud.Interfaces;

namespace TagsCloud
{
    public class PrintSettings : IPrintSettings
    {
        public Pen CentralPen { get; private set; }
        public Pen SurroundPen { get; private set; }
        public Color Background { get; private set; }
        public string FontName { get; private set; }
        public int FontSize { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public void SetFont(string fontName, int fontSize)
        {
            FontName = fontName;
            FontSize = fontSize;
        }

        public void SetCentralPen(Color color, int penWidth)
        {
            CentralPen = new Pen(color, penWidth);
        }

        public void SetSurroundPen(Color color, int penWidth)
        {
            SurroundPen = new Pen(color, penWidth);
        }

        public void SetBackgroudColor(Color background)
        {
            Background = background;
        }

        public void SetPictureSize(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
