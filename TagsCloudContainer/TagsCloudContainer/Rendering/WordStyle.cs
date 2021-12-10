using System;
using System.Drawing;

namespace TagsCloudContainer.Rendering
{
    public class WordStyle : IDisposable
    {
        public string Value { get; }
        public Font Font { get; }
        public Point Location { get; }
        public Brush Brush { get; }

        public WordStyle(string value, Font font, Point location, Brush brush)
        {
            Value = value;
            Font = font;
            Location = location;
            Brush = brush;
        }

        public void Dispose()
        {
            Font.Dispose();
            Brush.Dispose();
        }
    }
}