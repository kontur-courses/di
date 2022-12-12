using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;
using TagsCloudVisualization.CloudLayouter;
using TagsCloudVisualization.Spirals;

namespace TagsCloudVisualization.TagsCloudVisualizationTests
{
    [TestFixture]
    public class CircularCloudLayouterTests
    {
        private Point center;
        private Spiral spr;
        private List<Rectangle> addedRectangles;
        private CircularCloudLayouter layouter;

        [SetUp]
        public void SetUp()
        {
            center = new Point(400, 400);
            spr = new Spiral(center);
            addedRectangles = new List<Rectangle>();
            layouter = new CircularCloudLayouter(spr);
        }

        [TestCase(1, -1, TestName = "Negative height, correct width")]
        [TestCase(-1, 1, TestName = "Negative width, correct height")]
        [TestCase(1, 0, TestName = "Zero height, correct width")]
        [TestCase(0, 1, TestName = "Zero width, correct height")]
        public void PutNextRectangle_ThrowsArgumentException_IncorrectArguments(int width, int height)
        {
            var action = () => layouter.PutNextRectangle(new Size(width, height));
            action.Should().Throw<ArgumentException>()
                .WithMessage("Sides of the rectangle should not be non-positive");
        }

        [Test]
        public void PutNextRectangle_TopLeftCornerShouldBeInCenter_WhenOneRectangleSizeWasGiven()
        {
            var rectangle = layouter.PutNextRectangle(new Size(20, 10));
            var topLeftCorner = new Point(rectangle.Left, rectangle.Top);
            topLeftCorner.Should().Be(center);
        }

        private static bool RectanglesIntersect(IEnumerable<Rectangle> rectangles)
        {
            return rectangles.Any(rectangle1 => rectangles.Any(rectangle2 =>
                rectangle2.IntersectsWith(rectangle1) && !rectangle1.Equals(rectangle2)));
        }
    }
}