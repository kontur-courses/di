using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudGenerator.Tools;

namespace TagsCloudGeneratorTests.ToolsForTests
{
    public class GeometryHelperTests
    {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void BuildConvexHull_WithLessThanThreePoints_ShouldThrowArgumentException(int pointsCount)
        {
            var points = Enumerable.Repeat(new Point(10, 10), pointsCount).ToList();

            Action act = () => GeometryHelper.BuildConvexHull(points);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void BuildConvexHull_WithThreePoints_ShouldReturnThemselves()
        {
            var points = new List<Point>
            {
                new Point(0, 0),
                new Point(1, 1),
                new Point(2, 0)
            };

            var actual = GeometryHelper.BuildConvexHull(points);

            actual.Should().BeEquivalentTo(points);
        }

        [Test]
        public void BuildConvexHull_ForRectangle_ShouldReturnRectanglePoints()
        {
            var rectangle = new Rectangle(new Point(1, 1), new Size(5, 6));

            var actual = GeometryHelper.BuildConvexHull(new List<Rectangle> { rectangle });

            actual.Should().BeEquivalentTo(rectangle.Vertexes());
        }

        [Test]
        public void BuildConvexHull_ForPointsAtLine_ShouldReturnBoundPoints()
        {
            var points = new List<Point>
            {
                new Point(0, 0),
                new Point(1, 1),
                new Point(2, 2),
                new Point(4, 4)
            };
            var expected = new List<Point>
            {
                new Point(0, 0),
                new Point(4, 4)
            };

            var actual = GeometryHelper.BuildConvexHull(points);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void BuildConvexHull_ForSeveralPoints_ShouldReturnRightHull()
        {
            var points = new List<Point>
            {
                new Point(0, 0),
                new Point(5, 7),
                new Point(10, 3),
                new Point(11, -5),
                new Point(2, -3),
                new Point(-4, 5),
                new Point(-10, -8)
            };
            var expected = new List<Point>
            {
                new Point(5, 7),
                new Point(10, 3),
                new Point(11, -5),
                new Point(-4, 5),
                new Point(-10, -8)
            };

            var actual = GeometryHelper.BuildConvexHull(points);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void BuildConvexHull_ForSeveralRectangles_ShouldReturnRightHull()
        {
            var rectangles = new List<Rectangle>
            {
                new Rectangle(-1, -1, 3, 2),
                new Rectangle(3, -1, 2, 2),
                new Rectangle(-1, 2, 4, 2),
                new Rectangle(2, -5, 2, 3),
                new Rectangle(-2, -3, 4, 1),
                new Rectangle(-3,0,1,1),
                new Rectangle(-5,-1,1,2)
            };
            var expected = new List<Point>
            {
                new Point(-2, -3),
                new Point(5, -1),
                new Point(4, -5),
                new Point(5, 1),
                new Point(3, 4),
                new Point(-1, 4),
                new Point(-5, 1),
                new Point(2,-5),
                new Point(-5,-1)
            };

            var actual = GeometryHelper.BuildConvexHull(rectangles);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetSquareOfConvexHull_WithTriangle_ShouldReturnRightValue()
        {
            var hull = new List<Point>
            {
                new Point(0, 0),
                new Point(4, 0),
                new Point(0, 4)
            };

            var actual = GeometryHelper.GetSquareOfConvexHull(hull);

            actual.Should().BeApproximately(8, 0.000001);
        }

        [Test]
        public void GetSquareOfConvexHull_WithRectangle_ShouldReturnRightValue()
        {
            var hull = new List<Point>
            {
                new Point(0, 0),
                new Point(4, 0),
                new Point(4, 4),
                new Point(0, 4)
            };

            var actual = GeometryHelper.GetSquareOfConvexHull(hull);

            actual.Should().BeApproximately(16, 0.000001);
        }

        [Test]
        public void GetSquareOfConvexHull_ComplexCase_ShouldReturnRightValue()
        {
            var points = new List<Point>
            {
                new Point(0, 0),
                new Point(1, 8),
                new Point(11, 8),
                new Point(17, 0),
                new Point(9, -3),
                new Point(1, -3),
            };
            var expected = 3.0 * (17 + 8) / 2 + 8 * (10 + 17) / 2.0;

            var actual = GeometryHelper.GetSquareOfConvexHull(points);

            actual.Should().BeApproximately(expected, 0.00001);
        }

        [Test]
        public void GetSquareOfConvexHull_HullOfTwoRectangles_ShouldReturnRightValue()
        {
            var hull = new List<Point>
            {
                new Point(0, 0),
                new Point(1, -5),
                new Point(2, -5),
                new Point(4, 0),
                new Point(4, 3),
                new Point(0, 3)
            };
            var expected = 4 * 3 + (1 + 4) * 0.5 * 5;

            var actual = GeometryHelper.GetSquareOfConvexHull(hull);

            actual.Should().BeApproximately(expected, 0.00001);
        }
    }
}