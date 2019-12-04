using System;
using System.Drawing;

namespace TagCloud
{
    public class CloudVisualization : ICloudVisualization
    {
        private readonly ICloud cloud;

        public CloudVisualization(ICloud cloud)
        {
            this.cloud = cloud;
        }

        public Bitmap GetAndDrawRectangles(int width = 1000, int height = 1000, string path = null)
        {
            var rectangles = cloud.GetRectangles(width, height, path);
            var image = new Bitmap(width, height);
            var drawPlace = Graphics.FromImage(image);
            var random = new Random();
            foreach (var rectangle in rectangles)
            {
                var font = new Font(FontFamily.GenericMonospace, (float) rectangle.FSize, FontStyle.Italic);
                var color = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
                drawPlace.DrawString(rectangle.Text, font,new SolidBrush(color),rectangle.Area.Location);
            }
            return image;
        }
    }
}