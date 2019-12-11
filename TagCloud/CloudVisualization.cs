using System.Collections.Generic;
using System.Drawing;
using TagCloud.IServices;
using TagCloud.Models;

namespace TagCloud
{
    public class CloudVisualization : ICloudVisualization
    {
        private readonly ICloud cloud;

        public CloudVisualization(ICloud cloud, IPaletteDictionaryFactory paletteDictionaryFactory)
        {
            PaletteDictionary = paletteDictionaryFactory.GetPaletteDictioanry();
            this.cloud = cloud;
        }

        public Dictionary<string, Palette> PaletteDictionary { get; }

        public Bitmap GetAndDrawRectangles(ImageSettings imageSettings, string path = "test.txt")
        {
            var palette = PaletteDictionary[imageSettings.PaletteName];
            var image = new Bitmap(imageSettings.Width, imageSettings.Height);
            using (var graphics = Graphics.FromImage(image))
            {
                var rectangles =
                    RectanglesCustomizer.GetRectanglesWithPalette(palette,
                        cloud.GetRectangles(graphics, imageSettings, path));
                foreach (var rectangle in rectangles)
                    graphics.DrawString(rectangle.Tag.Text, rectangle.Tag.Font, new SolidBrush(rectangle.Color),
                        rectangle.Area.Location);
            }

            return image;
        }
    }
}