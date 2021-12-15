using System;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Extensions;

namespace TagsCloudVisualization
{
    public class TagCloudVisualiser : ITagCloudVisualiser
    {
        public Image Render(Tag[] tags, Size resolution)
        {
            var points = tags.SelectMany(t => t.Location.GetPoints()).ToArray();
            var center = new PointF(points.Average(p => p.X), points.Average(p => p.Y));
            var radius = points.Max(p => p.DistanceTo(center));
            var scale = Math.Min(resolution.Width, resolution.Height) / (radius * 2);
            var image = new Bitmap(resolution.Width, resolution.Height);
            using var graphics = Graphics.FromImage(image);
            graphics.ScaleTransform(scale, scale);
            graphics.TranslateTransform( resolution.Width  / scale / 2, resolution.Height / scale / 2 );
            foreach (var tag in tags)
            {
                //graphics.FillRectangle(Brushes.Red, tag.Location);
                //graphics.DrawRectangle(new Pen(Color.Blue, 5 / scale), tag.Location);
                var (textImage, frame) = RenderTag(tag, scale);
                graphics.DrawImage(textImage, tag.Location, frame, GraphicsUnit.Pixel);
            }
            return image;
        }

        private (Image textImage, RectangleF frame) RenderTag(Tag tag, float renderScale)
        {
            var scale = 10 * renderScale * Math.Min(tag.Location.Width, tag.Location.Height) / Math.Min(tag.ValueSize.Width, (tag.ValueSize.Height));
            var image = new Bitmap((int)(tag.ValueSize.Width * scale), (int)(tag.ValueSize.Height * scale ));
            using (var graphics = Graphics.FromImage(image))
            {
                graphics.ScaleTransform(scale, scale);
                var sf = new StringFormat() { Alignment = StringAlignment.Near, Trimming = StringTrimming.None };
                graphics.DrawString(tag.Value, tag.Font, new SolidBrush(tag.Color), 0, 0, sf);
            }
            var transparent = Color.FromArgb(0, 0, 0, 0);
            var start = new Point(int.MaxValue, int.MaxValue);
            var end = Point.Empty;
            for (var i = 0; i < image.Width; i++)
            for (var j = 0; j < image.Height; j++)
            {
                if (image.GetPixel(i, j) == transparent) continue;
                start.X = Math.Min(start.X, i);
                start.Y = Math.Min(start.Y, j);
                end.X = Math.Max(end.X, i);
                end.Y = Math.Max(end.Y, j);
            }
            var size = new Size(end.X - start.X, end.Y - start.Y);
            return (image, new RectangleF(start, size));
        }
    }
}