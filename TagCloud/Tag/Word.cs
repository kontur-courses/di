using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagCloud.Tag
{
    public class Word : Layout, ITag
    {
        private static readonly Graphics Meter = Graphics.FromImage(new Bitmap(1,1)); 
        public string Text { get; }

        public Font Font { get; }

        public Word(string word, Font font) :base(GetTextLayout(word, font))
        {
            Text = word;
            Font = font;
            
        }

        private static Size GetTextLayout(string word, Font font)
        {
            var size = Meter.MeasureString(word, font);
            return new Size(
                width: (int)Math.Ceiling(size.Width),
                height:(int)Math.Ceiling(size.Height));
        }
    }
}
