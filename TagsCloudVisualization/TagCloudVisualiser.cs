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
            var center = new PointF(allPoints.Average(p => p.X), allPoints.Average(p => p.Y));
            var radius = allPoints.Max(p => p.DistanceTo(center));
            var scale = (float)Math.Min(resolution.Width, resolution.Height) / (radius * 2);
            var image = new Bitmap(resolution.Width, resolution.Height);
            var graphics = Graphics.FromImage(image);
            graphics.ScaleTransform(scale / 2, scale / 2);
            graphics.TranslateTransform( resolution.Width  / scale - center.X, resolution.Height / scale - center.Y );
            foreach (var tag in tags)
            {
                graphics.FillRectangle(Brushes.Blue, tag.Location);
                graphics.DrawRectangle(new Pen(Color.Black, 5 / scale), tag.Location);
                graphics.DrawString(tag.Value, tag.Font, new SolidBrush(tag.Color), tag.Location);
            }
            return image;
        }
    }
}