using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudContainer.Layouter;
using TagsCloudContainer.Tests;

namespace TagsCloudContainer.Tests
{
    class CircularCloudLayouterTests
    {
        private CircularCloudLayouter layouter;
        private readonly Point defaultCenter = new Point(0, 0);

        [SetUp]
        public void CreateCircularCloudLayouter()
        {
            layouter = new CircularCloudLayouter(defaultCenter);
        }

        [Test]
        public void PutNextRectangle_ShouldPutRectangleWithSameSizeAsInArgument_IfFirstRectangle()
        {
            var rectangleSize = new Size(10, 10);

            layouter.PutNextRectangle(rectangleSize);
            var rectangle = layouter.Rectangles[0];
            var size = rectangle.Size;

            size.Should().Be(rectangleSize);
        }

        [Test]
        [TestCase(10, 10)]
        [TestCase(9, 9)]
        public void PutNextRectangle_ShouldPutRectangleInCenter_IfFirstRectangle(int width, int height)
        {
            var rectangleSize = new Size(width, height);

            var rectangle = layouter.PutNextRectangle(rectangleSize);
            var location = rectangle.Location;

            var expectedLocation = new Point(-width / 2, -height / 2);
            location.Should().Be(expectedLocation);
        }

        [TestCase(100)]
        [TestCase(200)]
        [TestCase(300)]
        [TestCase(400)]
        [TestCase(500)]
        public void PutNextRectangle_RectanglesShouldBeArrangedAsCircle(int seed)
        {
            var sizes = Generator.GetRandomSizesList(10, 100, 10, 100, 500, new Random(seed));
            var rectangelsArea = 0;

            foreach (var size in sizes)
            {
                var rectangle = layouter.PutNextRectangle(size);
                rectangelsArea += rectangle.Width * rectangle.Height;
            }

            var squaredMaxRadius = (int)Math.Ceiling(rectangelsArea / Math.PI);
            squaredMaxRadius += squaredMaxRadius / 3;
            var corners = layouter.Rectangles
                .SelectMany(rectangle => rectangle.GetCorners()).ToList();
            foreach (var corner in corners)
                corner.SquaredDistanceTo(layouter.Center).Should().BeLessThan(squaredMaxRadius);
        }

        [TestCase(100)]
        [TestCase(200)]
        [TestCase(300)]
        [TestCase(400)]
        [TestCase(500)]
        public void PutNextRectangle_RectanglesShouldNotIntersect(int seed)
        {
            var sizes = Generator.GetRandomSizesList(10, 100, 10, 100, 100, new Random(seed));

            foreach (var size in sizes)
                layouter.PutNextRectangle(size);

            var rectangles = layouter.Rectangles;
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