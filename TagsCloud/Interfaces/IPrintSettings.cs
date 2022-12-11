using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloud.Interfaces
{
    public interface IPrintSettings
    {
        public Pen CentralPen { get; }
        public Pen SurroundPen { get; }
        public Color Background { get; }
        public string FontName { get; }
        public int FontSize { get; }
        public int Width { get; }
        public int Height { get; }

        public void SetFont(string fontName, int fontSize);

        public void SetCentralPen(Color color, int penWidth);

        public void SetSurroundPen(Color color, int penWidth);

        public void SetBackgroudColor(Color background);

        public void SetPictureSize(int width, int height);
    }
}
