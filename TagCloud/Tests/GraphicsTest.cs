using System.Collections.Generic;
using System.Drawing;
using System.IO;
using NUnit.Framework;

namespace TagsCloud.Tests
{
    public class GraphicsTest
    {
        [Test]
        public void SaveMap_CreatePng()
        {
            var words = new Dictionary<string, double>
            {
                {"car", 56},
                {"auto", 20},
                {"automobile", 24}
            };
            var layout = new CreateLayout(new CircularCloudLayouter(new Point(0, 0)));
            var wordWithCoordinate = layout.GetLayout(words);
            var color = Color.Black;
            var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "image");
            var graphics = new CoordinatesAtImage(path, "Arial", color, new Size(1000, 1000));
            graphics.GetCoordinates(wordWithCoordinate);
        }
    }
}