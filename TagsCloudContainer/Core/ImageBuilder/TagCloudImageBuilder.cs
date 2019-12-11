using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.Core.ColoringAlgorithms;

namespace TagsCloudContainer.Core.ImageBuilder
{
    class TagCloudImageBuilder : IImageBuilder
    {
        private readonly Brush brush;
        private readonly Pen pen;
        private readonly StringFormat stringFormat;
        private readonly IColoringAlgorithm coloringAlgorithm;

        public TagCloudImageBuilder(IColoringAlgorithm coloringAlgorithm)
        {
            brush = new SolidBrush(Color.White);
            pen = new Pen(Color.Blue);
            stringFormat = new StringFormat()
            {
                FormatFlags = StringFormatFlags.NoWrap,
                Trimming = StringTrimming.None
            };
            this.coloringAlgorithm = coloringAlgorithm;
        }

        public Bitmap Build(string fontName, IEnumerable<Tag> tags, Size size)
        {
            var bitmap = new Bitmap(size.Width, size.Height);
            var graphics = Graphics.FromImage(bitmap);
            graphics.FillRectangle(brush, new Rectangle(0, 0, size.Width, size.Height));

            foreach (var tag in tags)
            {
                var currentColor = coloringAlgorithm.GetNextColor();
                graphics.DrawString(tag.Word, new Font(fontName, tag.FontSize),
                    new SolidBrush(currentColor), tag.Rectangle, stringFormat);
            }

            return bitmap;
        }
    }
}