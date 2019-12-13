using System;
using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Algorithm.SpiralBasedLayouter;

namespace TagCloudTests.Algorithm.SpiralBasedLayouter
{
    public class GeometryUtilsTests
    {
        [TestCase(6, Math.PI / 2, 0, 6)]
        [TestCase(5, 0, 5, 0)]
        [TestCase(3, Math.PI, -3, 0)]
        public void ConvertPolarToIntegerCartesian_ShouldReturnCorrectResult_OnPolar(
            double r, double phi, int x, int y)
        {
            var expectedPoint = new Point(x, y);

            var result = GeometryUtils.ConvertPolarToIntegerCartesian(r, phi);

            result.Should().Be(expectedPoint);
        }

        [Test]
        public void BuildConvexHull_ShouldReturnRectangleCorners_OnOneRectangle()
        {
            var rectangle = new Rectangle(new Point(1, 1), new Size(3, 1));
            var rectangles = new List<Rectangle> { rectangle };
            var expectedPoints = RectangleUtils.GetRectangleCorners(rectangle);

            var result = GeometryUtils.BuildConvexHull(rectangles);

            result.Should().BeEquivalentTo(expectedPoints);
        }

        [Test]
        public void BuildConvexHull_ShouldReturnConvexHull_OnManyRectangles()
        {
            var rectangles = new List<Rectangle>
            {
                new Rectangle(new Point(0, -1), new Size(7, 1)),
                new Rectangle(new Point(-6, 1), new Size(5, 3)),
                new Rectangle(new Point(-4, -2), new Size(3, 2)),
                new Rectangle(new Point(1, -3), new Size(3, 1)),
                new Rectangle(new Point(1, 1), new Size(3, 1))
            };
            var convexHull = new List<Point>
            {
                new Point(-6, 4),
                new Point(-6, 1),
                new Point(-4, -2),
                new Point(1, -3),
                new Point(4, -3),
                new Point(7, -1),
                new Point(7, 0),
                new Point(4, 2),
                new Point(-1, 4)
            };

            var result = GeometryUtils.BuildConvexHull(rectangles);

            result.Should().BeEquivalentTo(convexHull);
        }
    }
}
