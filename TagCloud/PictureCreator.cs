using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud
{
    public class PictureCreator
    {
        private readonly FontFamily fontFamily;
        private readonly Color wordColor;
        private readonly IReadOnlyCollection<Tag> tags;
        private readonly Graphics graphics;

        public PictureCreator(FontFamily fontFamily, Color wordColor, IReadOnlyCollection<Tag> tags, Graphics graphics)
        {
            this.fontFamily = fontFamily;
            this.wordColor = wordColor;
            this.tags = tags;
            this.graphics = graphics;
        }

        public void DrawPicture()
        {
            var drawFormat = new StringFormat
            {
                FormatFlags = StringFormatFlags.DirectionRightToLeft,
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            foreach (var tag in tags)
                using (var brush = new SolidBrush(wordColor))
                {
                    var currentFont = new Font(fontFamily, tag.WordBox.Height);
                    graphics.DrawString(tag.Word, currentFont, brush, tag.WordBox, drawFormat);
                }
        }
    }
}