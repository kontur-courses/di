using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web.UI;
using TagCloud.Models;

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
                foreach (var rectangle in rectangles)
                {
                    graphics.DrawString(rectangle.Tag.Text, rectangle.Tag.Font, new SolidBrush(color), rectangle.Area.Location);
                }
            }

            return image;
        }
    }
}