using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Extensions;

namespace TagsCloudVisualization
{
    public class TagCloudVisualiser : ITagCloudVisualiser
    {
        public Image Render(Tag[] tags, Size resolution)
        {
            var allPoints = tags.SelectMany(t => t.Location.GetPoints()).ToArray();
            var center = PointF.Empty;
            foreach (var point in allPoints)
            {
                center.X += point.X;
                center.X += point.Y;
            }
            center.X /= allPoints.Length;
            center.Y /= allPoints.Length;
            var radius = allPoints.Max(p => p.DistanceTo(center));
            var scale = Math.Min(resolution.Width, resolution.Height) / (radius * 2);
            var image = new Bitmap(resolution.Width, resolution.Height);
            var graphics = Graphics.FromImage(image);
            graphics.ScaleTransform(scale  , scale);
            graphics.TranslateTransform( resolution.Width  / scale / 2, resolution.Height / scale / 2 );
            foreach (var tag in tags)
            {
                //graphics.FillRectangle(Brushes.Red, tag.Location);
                //graphics.DrawRectangle(new Pen(Color.Blue, 5 / scale), tag.Location);
                var tagImage = RenderTag(tag);
                graphics.DrawImage(tagImage, tag.Location);
            }
            graphics.Dispose();
            return image;
        }

        private Image RenderTag(Tag tag)
        {
            var scale = 10;
            var image = new Bitmap((int)(tag.Location.Width * scale), (int)(tag.Location.Height * scale));
            var g = Graphics.FromImage(image);
            g.ScaleTransform(scale, scale);
            var sf = new StringFormat(){Alignment = StringAlignment.Near, Trimming = StringTrimming.None};
            g.DrawString(tag.Value, tag.Font,  new SolidBrush(tag.Color), 0, 0, sf);
            g.Dispose();
            var transparent = Color.FromArgb(0, 0, 0, 0);
            var start = new Point(Int32.MaxValue, Int32.MaxValue);
            var end = Point.Empty;
            for (var i = 0; i < image.Width; i++)
            for (var j = 0; j < image.Height; j++)
            {
                if (image.GetPixel(i, j) != transparent)
                {
                    start.X = Math.Min(start.X, (int)(i ));
                    start.Y = Math.Min(start.Y, (int)(j ));
                    end.X = Math.Max(end.X, (int)(i ));
                    end.Y = Math.Max(end.Y, (int)(j ));
                }
            }
            var size = new Size(end.X - start.X, end.Y - start.Y);
            var newImage = new Bitmap(size.Width, size.Height);
            g = Graphics.FromImage(newImage);
            g.DrawImage(image, new Rectangle(Point.Empty, newImage.Size),
                new Rectangle(start, size), GraphicsUnit.Pixel);
            g.Dispose();
            return newImage;
        }
    }
}