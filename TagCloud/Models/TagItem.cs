using System;

namespace TagCloud.Models
{
    public struct TagItem
    {
        public string Word { get; }
        public int FontSize { get; }

        public TagItem(string word, int fontSize)
        {
            if (fontSize <= 0)
                throw new ArgumentException("Font size can't be less or equal to 0");
            Word = word;
            FontSize = fontSize;
        }
    }
}