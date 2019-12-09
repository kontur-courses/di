using System;
using System.Drawing;
using System.IO;
using System.Web.UI;

namespace TagCloud
{
    public class CloudVisualization : ICloudVisualization
    {
        private readonly ICloud cloud;

        public CloudVisualization(ICloud cloud)
        {
            this.cloud = cloud;
        }

        public Bitmap GetAndDrawRectangles(int width = 1000, int height = 1000, string path = "test.txt")
        {
            var image = new Bitmap(width, height);
            using (var graphics = Graphics.FromImage(image))
            {
                var rectangles = cloud.GetRectangles( graphics,width, height, path);
                var random = new Random();
                foreach (var rectangle in rectangles)
                {
                    var color = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
                    graphics.DrawString(rectangle.Tag.Text, rectangle.Tag.Font, new SolidBrush(color), rectangle.Area.Location);
                }
            }

            return image;
        }
    }
}