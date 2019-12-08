using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudGenerator
{
    public static class CloudImageCreator
    {
        public static Bitmap CreateTagCloudBitmap(IEnumerable<CloudTag> tags, Size imageSize, CloudFormat format)
        {
            var bitmap = new Bitmap(imageSize.Width, imageSize.Height);
            var graphics = Graphics.FromImage(bitmap);

            var textPen = new Pen(Color.Black);
            var rectPen = new Pen(Color.Black);

            foreach (var tag in tags)
            {
                rectPen.Color = format.ColorPainter.GetTagShapeColor();
                graphics.DrawRectangle(rectPen, tag.Shape);

                textPen.Color = format.ColorPainter.GetTagTextColor(rectPen.Color);
                graphics.DrawString(tag.Text, tag.TextFont, textPen.Brush, 
                    tag.Shape.ConvertToRectangleF(), tag.Format);
            }

            var backgroundPen = new Pen(format.ColorPainter.BackgroundColor);
            graphics.DrawRectangle(backgroundPen, 0, 0, imageSize.Width, imageSize.Height);

            return bitmap;
        }
    }
}