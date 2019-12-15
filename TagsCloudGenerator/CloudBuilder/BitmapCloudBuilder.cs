using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudGenerator
{
    public class BitmapCloudBuilder : ICloudBuilder<Bitmap>
    {
        public Bitmap CreateTagCloudFromTags(IEnumerable<CloudTag> tags, Size imageSize, CloudFormat format)
        {
            var bitmap = new Bitmap(imageSize.Width, imageSize.Height);
            var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(format.ColorPainter.BackgroundColor);

            var textPen = new Pen(Color.Black);
            var rectPen = new Pen(Color.Black);

            foreach (var tag in tags)
            {
                rectPen.Color = format.ColorPainter.GetTagShapeColor();
                graphics.DrawRectangle(rectPen, tag.Shape);

                textPen.Color = format.ColorPainter.GetTagTextColor(rectPen.Color);
                var text = format.TagTextPreform.PreformToVisualize(tag.Text);

                graphics.DrawString(text, tag.TextFont, textPen.Brush, 
                    tag.Shape.ConvertToRectangleF(), tag.Format);
            }

            return bitmap;
        }
    }
}