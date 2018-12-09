using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using NUnit.Framework;

namespace TagsCloud.Tests
{
    public class TagCloudRenderTests
    {
        [Test]
        public void Render_CreateImage()
        {
            var words = new List<string>
            {
                "car",
                "auto",
                "automobile"
            };
            var width = 0.1;
            var step = 0.01;
            var center = new Point(0, 0);
            var layout =
                new TagCloudLayouter(new CircularCloudLayouter(center, new CircularSpiral(center, width, step)));
            var color = Color.Black;
            var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "image");
            var coordinatesAtImage = new CoordinatesAtImage(new Size(1000, 1000));
            var frequencyDictionary = new FrequencyCollection();
            var picture = new Picture(new Size(800, 800), new FontFamily("Arial"), color, ImageFormat.Jpeg, path);
            var render = new TagCloudRender(layout, coordinatesAtImage, new ConstWordCollection(words),
                frequencyDictionary, picture);
            render.Render();
        }
    }
}