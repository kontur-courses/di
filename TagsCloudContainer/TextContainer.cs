using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Windows.Forms.VisualStyles;

namespace TagsCloudContainer
{
    public class TextContainer
    {
        public string Text { get; private set; }
        public Point Point { get; private set; }
        public Font Font { get; private set; }

        public TextContainer(string text, Point point, Font font)
        {
            Text = text;
            Point = point;
            Font = font;
        }
    }
}