using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;
using TagsCloudVisualization.Default;

namespace TagCloudTests
{
    [TestFixture]
    public class TagCloudMakerTests
    {
        [TestCase("abc", 1, "Impact")]
        [TestCase("abrakodabra", 1, "Impact")]
        [TestCase("Tag Cloud", 25, "Arial")]
        [TestCase("l", 90, "Arial")]
        [TestCase("l", 0.5, "Arial")]
        [TestCase("Very very big and long tag", 50, "Comic Sans MS")]
        public void Maker_TagSquare_SameAsWeight(string value, double weight, string fontName)
        {
            var font = new Font(fontName, 10);
            var tokens = new [] { new Token(value, weight) };
            var tagCloud = new TagCloudMaker(new CircularCloudMaker(Point.Empty), new RandomTagColor());
            var tag = tagCloud.CreateTagCloud(tokens, font).First();
            var square = tag.Location.Width * tag.Location.Height;
            square.Should().BeApproximately((float)weight, 0.01f);
        }
    }
}