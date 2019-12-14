using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Layouter;

namespace TagsCloudContainer.OldTests
{
    class CircularCloudLayouterTests
    {
        private CircularCloudLayouter layouter;

        [SetUp]
        public void CreateCircularCloudLayouter()
        {
            layouter = new CircularCloudLayouter(new DefaultLayouterSettings());
        }

        [Test]
        public void PutNextRectangle_ShouldPutRectangleWithSameSizeAsInArgument_IfFirstRectangle()
        {
            var rectangleSize = new Size(10, 10);
            var sizes = new List<Size> { rectangleSize };

            var rectangle = layouter.GetRectangles(sizes)[0];
            var size = rectangle.Size;

            size.Should().Be(rectangleSize);
        }

        [Test]
        [TestCase(10, 10)]
        [TestCase(9, 9)]
        public void GetRectangles_ShouldPutRectangleInCenter_IfFirstRectangle(int width, int height)
        {
            var sizes = new List<Size> { new Size(width, height) };

            var rectangle = layouter.GetRectangles(sizes)[0];
            var location = rectangle.Location;

            var expectedLocation = new Point(-width / 2, -height / 2);
            location.Should().Be(expectedLocation);
        }

        [TestCase(100)]
        [TestCase(200)]
        [TestCase(300)]
        [TestCase(400)]
        [TestCase(500)]
        public void GetRectangles_RectanglesShouldBeArrangedAsCircle(int seed)
        {
            var sizes = Generator.GetRandomSizesList(10, 100, 10, 100, 500, new Random(seed));

            var rectangles = layouter.GetRectangles(sizes);
            var rectanglesArea = rectangles.Sum(rectangle => rectangle.Width * rectangle.Height);

            var squaredMaxRadius = (int)Math.Ceiling(rectanglesArea / Math.PI);
            squaredMaxRadius += squaredMaxRadius / 3;
            var corners = rectangles
                .SelectMany(rectangle => rectangle.GetCorners()).ToList();
            foreach (var corner in corners)
                corner.SquaredDistanceTo(layouter.Center).Should().BeLessThan(squaredMaxRadius);
        }

        [TestCase(100)]
        [TestCase(200)]
        [TestCase(300)]
        [TestCase(400)]
        [TestCase(500)]
        public void GetRectangles_RectanglesShouldNotIntersect(int seed)
        {
            var sizes = Generator.GetRandomSizesList(10, 100, 10, 100, 100, new Random(seed));

            var rectangles = layouter.GetRectangles(sizes);
            for (var i = 0; i < rectangles.Count; i++)
            {
                for (var j = 0; j < rectangles.Count; j++)
                {
                    if (i == j)
                        continue;
                    rectangles[i].IntersectsWith(rectangles[j]).Should().BeFalse();
                }
            }
        }
    }
}