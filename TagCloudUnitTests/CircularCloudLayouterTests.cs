using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagCloud.CloudLayouter;
using TagCloud.ImageProcessing;
using TagCloud.PointGenerator;

namespace TagCloudUnitTests
{
    [TestFixture]
    internal class CircularCloudLayouterTests
    {
        private CircularCloudLayouter layouter;

        private IList<Rectangle> layout;

        [SetUp]
        public void Setup()
        {
            layouter = new CircularCloudLayouter(new CirclePointGenerator(new Point(0, 0)));

            layout = new List<Rectangle>();
        }

        [Test]
        public void PutNextRectangle_PutsRectangleInTheCenter_WhenFirstRectangleAdded()
        {
            layout = GetLayout(new[] { new Size(100, 50) });

            layout.First().GetCenter().Should().BeEquivalentTo(layouter.CloudCenter);
        }

        [TestCase(-1, 1, TestName = "Negative width")]
        [TestCase(1, -1, TestName = "Negative height")]
        [TestCase(-1, -1, TestName = "Negative width and height")]
        [TestCase(0, 1, TestName = "Zero width")]
        [TestCase(1, 0, TestName = "Zero height")]
        [TestCase(0, 0, TestName = "Zero width and height")]
        public void PutNextRectangle_ThrowsArgumentException_WhenSizeIsInvalid(int width, int height)
        {
            Action action = () => layouter.PutNextRectangle(new Size(width, height));

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void PutNextRectangle_PutsTwoAlignedHorizontallyRectangles_WhenTwoAdded()
        {
            var sizes = new[]
            {
                new Size(100, 150),
                new Size(100, 150)
            };

            layout = GetLayout(sizes);

            var firstRectangle = layout[0];
            var secondRectangle = layout[1];

            Math.Abs(firstRectangle.Y).Should().Be(Math.Abs(secondRectangle.Y));
        }

        [Test]
        public void PutNextRectangle_PutsTwoAlignedVerticallyRectangles_WhenTwoAdded()
        {
            var sizes = new[]
            {
                new Size(150, 100),
                new Size(150, 100)
            };

            layout = GetLayout(sizes);

            var firstRectangle = layout[0];
            var secondRectangle = layout[1];

            Math.Abs(firstRectangle.X).Should().Be(Math.Abs(secondRectangle.X));
        }
        
        [TestCase(2, TestName ="Two rectangles")]
        [TestCase(10, TestName = "Ten rectangles")]
        [TestCase(100, TestName = "One hundred rectangles")]
        [TestCase(1000, TestName = "One thousand rectangles")]
        public void PutNextRectangle_PutsNonIntersectingRectangles_WhenSeveralRectanglesAdded(int rectanglesCount)
        {
            var sizes = RectangleSizeGenerator.GetConstantSizes(rectanglesCount, new Size(100, 50));

            layout = GetLayout(sizes);

            IsAnyIntersection(layout).Should().Be(false);
        }

        private bool IsAnyIntersection(IList<Rectangle> cloudLayout)
        {
            foreach (var rectangle in cloudLayout)
                if (cloudLayout.Where(r => !r.Equals(rectangle)).Any(r => r.IntersectsWith(rectangle)))
                    return true;

            return false;
        }

        private IList<Rectangle> GetLayout(IEnumerable<Size> rectanglesSizes)
        {
            var rectangles = rectanglesSizes.Select(size => layouter.PutNextRectangle(size)).ToList();

            return rectangles;
        }
    }
}