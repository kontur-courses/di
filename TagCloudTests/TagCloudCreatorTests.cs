using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TagCloud
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

            var circularCloudLayouter = new CircularCloudLayouter(center);
            foreach (var rectangleSize in rectangleSizes)
                circularCloudLayouter.PutNextRectangle(rectangleSize);

            circularCloudLayouter.GetTagCloud().Should().Be(
                TagCloudCreator.Create(rectangleSizes, center));
        }
    }
}
