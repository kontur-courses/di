using System.Collections.Generic;
using System.Drawing;
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
            var layout = new CreateLayout(new CircularCloudLayouter(new Point(0, 0)));
            var color = Color.Black;
            var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "image");
            var graphics = new Graphics(path, "Arial", color, new Size(1000, 1000));
            var frequencyDictionary = new FrequencyDictionary();
            var render = new TagCloudRender(layout, graphics, new ConstWordCollection(words), frequencyDictionary);
            render.Render();
        }
    }
}