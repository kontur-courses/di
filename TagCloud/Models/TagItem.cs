using System;
using System.Drawing;

namespace TagCloud.Models
{
    public class TagItem
    {
        public string Word { get; }
        public Size Size { get; }

        public TagItem(string word, Size size)
        {
            if (size.Width <= 0 || size.Height <= 0)
                throw new ArgumentException("Size can't be less or equal tp 0");
            Word = word;
            Size = size;
        }
    }
}