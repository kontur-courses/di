using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TagsCloudContainer.Core.ImageBuilder
{
    class TagCloudImageCreator : IImageBuilder
    {
        private readonly Brush brush;
        private readonly Pen pen;
        private readonly Brush wordBrush;
        private readonly StringFormat stringFormat;

        public TagCloudImageCreator(Brush wordBrush)
        {
            brush = new SolidBrush(Color.White);
            this.wordBrush = wordBrush;
            pen = new Pen(Color.Blue);
            stringFormat = new StringFormat()
            {
                FormatFlags = StringFormatFlags.NoWrap,
                Trimming = StringTrimming.None
            };
        }

        public Bitmap Build(string fontName, IEnumerable<Tag> tags, Size size)
        {
            var bitmap = new Bitmap(size.Width, size.Height);
            var graphics = Graphics.FromImage(bitmap);
            graphics.FillRectangle(brush, new Rectangle(0, 0, size.Width, size.Height));

            foreach (var tag in tags)
                graphics.DrawString(tag.Word, new Font(fontName, tag.FontSize), wordBrush, tag.Rectangle, stringFormat);

            return bitmap;
        }
    }
}