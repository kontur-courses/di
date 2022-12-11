using System;
using System.Drawing;
using TagCloud.CloudLayouters;

namespace TagCloud.Tags
{
    public class Word : Layout
    {
        private static readonly Graphics Meter = Graphics.FromImage(new Bitmap(1,1)); 
        public string Text { get; }

        public Font Font { get; }

        public Word(string word, Font font, ICloudLayouter circularCloudLayouter) : base(GetTextLayout(word, font))
        {
            Text = word;
            Font = font;
            frame = circularCloudLayouter.PutNextRectangle(Size);
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
