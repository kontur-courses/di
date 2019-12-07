using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudGenerator
{
    public static class CloudImageCreator
    {
        public static Bitmap CreateTagCloudBitmap(IEnumerable<CloudTag> tags, Size size, CloudFormat format)
        {
            var bitmap = new Bitmap(size.Width, size.Height);
            var graphics = Graphics.FromImage(bitmap);

            foreach (var tag in tags)
            {
                var rectPen = new Pen(format.ColorPainter.GetTagShapeColor());
                graphics.DrawRectangle(rectPen, tag.Shape);

                var textPen = new Pen(format.ColorPainter.GetTagTextColor());
                graphics.DrawString(tag.Text, tag.TextFont, textPen.Brush, 
                    tag.Shape.ConvertToRectangleF(), tag.Format);
            }

            var backgroundPen = new Pen(format.ColorPainter.BackgroundColor);
            graphics.DrawRectangle(backgroundPen, 0, 0, size.Width, size.Height);

            return bitmap;
        }
    }
}