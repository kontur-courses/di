using System;
using System.Drawing;

namespace TagCloud
{
    public static class TagCloudVisualization
    {
        public static void Visualizate(TagCloud cloud, string path)
        {
            var random = new Random();
            var bitmap = new Bitmap(2 * cloud.layouter.Size.Width, 2 * cloud.layouter.Size.Height);
            var vectorShift = new Point(
                cloud.layouter.Size.Width - cloud.layouter.Center.X, 
                cloud.layouter.Size.Height - cloud.layouter.Center.Y);
            var graphics = Graphics.FromImage(bitmap);
            foreach (var location in cloud)
            {
                var randomColor = Color.FromArgb(255, random.Next(255), random.Next(255), random.Next(255));
                var pen = new Pen(randomColor);
                var brush = new SolidBrush(randomColor);
                graphics.DrawRectangle(pen, ShiftRectangle(location.location));
                graphics.FillRectangle(brush, ShiftRectangle(location.location));
            }
            bitmap.Save(path);

            Rectangle ShiftRectangle(Rectangle r) =>
                new Rectangle(r.X + vectorShift.X, r.Y + vectorShift.Y, r.Width, r.Height);
        }
    }
}
