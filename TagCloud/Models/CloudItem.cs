using System;
using System.Drawing;

namespace TagCloud.Models
{
    public struct CloudItem
    {
        public string Word { get; }
        public Rectangle Bounds { get; }
        public Font Font { get; }

        public CloudItem(string word, Rectangle bounds,Font font)
        {
            if (bounds.Size.Width <= 0 || bounds.Size.Height <= 0)
                throw new ArgumentException(
                    $"Size can't be less or equal to 0, but was {bounds.Size.Height}x{bounds.Size.Width}");
            Word = word;
            Bounds = bounds;
            Font = font;
        }
    }
}