using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud;
using TagCloud.PointGenerators;
using TagCloud.Tag;

namespace TagCloudTests
{
    public class TagCloudCreatorTests
    {
        [TestCase(0,0)]
        [TestCase(-5, 10)]
        public void Create_MustCreateTagCloudByCircularCloudLayouter_Success(int centerX, int centerY)
        {
            var center = new Point(centerX, centerY);
            var rectangleSizes = Enumerable.Range(2, 40).
                Select(width => new Size(width, width / 2)).Reverse();

            var tagCloud = new TagCloud.TagCloud(center);
            var circularCloudLayouter = new CircularCloudLayouter(new SpiralPointGenerator(center));
            foreach (var rectangleSize in rectangleSizes)
                tagCloud.Rectangles.Add(new Layout(circularCloudLayouter.PutNextRectangle(rectangleSize)));

            tagCloud.Should().Be(TagCloudCreator.Create(rectangleSizes, center));
        }
    }
}
