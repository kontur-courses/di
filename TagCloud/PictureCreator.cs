using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud
{
    public class PictureCreator
    {
        private readonly FontFamily fontFamily;
        private readonly Graphics graphics;
        private readonly Size imageSize;
        private readonly IReadOnlyCollection<Tag> tags;
        private readonly Color wordColor;

        public PictureCreator(FontFamily fontFamily, Color wordColor, IReadOnlyCollection<Tag> tags, Graphics graphics,
            Size imageSize)
        {
            this.fontFamily = fontFamily;
            this.wordColor = wordColor;
            this.tags = tags;
            this.graphics = graphics;
            this.imageSize = imageSize;
        }

        public void DrawPicture()
        {
            var drawFormat = new StringFormat
            {
                FormatFlags = StringFormatFlags.DirectionRightToLeft,
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, imageSize.Width, imageSize.Height));
            foreach (var tag in tags)
                using (var brush = new SolidBrush(wordColor))
                {
                    var currentFont = new Font(fontFamily, tag.WordBox.Height);
                    graphics.DrawString(tag.Word, currentFont, brush, tag.WordBox, drawFormat);
                }
        }
    }
}