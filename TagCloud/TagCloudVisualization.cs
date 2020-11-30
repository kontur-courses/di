using System;
using System.Drawing;

namespace TagCloud
{
    public static class TagCloudVisualization
    {
        public static void Visualizate(TagCloud cloud, string path, string font = "Arial", Size? size = null)
        {
            var random = new Random();
            var bitmap = size == null ? new Bitmap(2 * cloud.layouter.Size.Width, 2 * cloud.layouter.Size.Height) :
                new Bitmap(size.Value.Width, size.Value.Height);
            var vectorShift = new Point(
                cloud.layouter.Size.Width - cloud.layouter.Center.X, 
                cloud.layouter.Size.Height - cloud.layouter.Center.Y);
            var graphics = Graphics.FromImage(bitmap);
            foreach (var location in cloud)
            {
                var randomColor = Color.FromArgb(255, random.Next(255), random.Next(255), random.Next(255));
                ;
                var brush = new SolidBrush(randomColor);
                graphics.DrawString(location.word, new Font(font, location.location.Height), 
                    brush, ShiftRectangle(location.location));
            }
            bitmap.Save(path);

            Rectangle ShiftRectangle(Rectangle r) =>
                new Rectangle(r.X + vectorShift.X, r.Y + vectorShift.Y, r.Width, r.Height);
        }
    }
}
