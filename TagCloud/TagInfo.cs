using System;
using System.Drawing;

namespace TagCloud
{
    public class TagInfo
    {
        public readonly string Value;
        public readonly Font Font;
        
        public TagInfo(string value, int fontSize)
        {
            Value = value;
            Font = new Font(FontFamily.GenericMonospace, fontSize);
        }
    }
}