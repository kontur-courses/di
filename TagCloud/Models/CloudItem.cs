using System;
using System.Drawing;

namespace TagCloud.Models
{

    public class CloudItem
    {
        public string Word { get; }
        public Rectangle Bounds { get; }

        public CloudItem(string word, Rectangle bounds)
        {
            if (bounds.Size.Width <= 0 || bounds.Size.Height <= 0)
                throw new ArgumentException("Size can't be less or equal tp 0");
            Word = word;
            Bounds = bounds;
        }
    }
}