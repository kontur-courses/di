using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Algorithm.SpiralBasedLayouter;

namespace TagCloudTests.Algorithm.SpiralBasedLayouter
{
    public class RectangleUtilsTests
    {
        [Test]
        public void GetPossibleRectangles_ShouldReturnRectanglesWithFourDifferentStartPoints_OnPointAndSize()
        {
            var point = new Point(0, 0);
            var size = new Size(2, 1);
            var possibleRectangles = new[]
            {
                new Rectangle(point, size),
                new Rectangle(new Point(-2, 0), size),
                new Rectangle(new Point(-2, -1), size),
                new Rectangle(new Point(0, -1), size),
            };

            var result = RectangleUtils.GetPossibleRectangles(point, size);

            result.Should().BeEquivalentTo(possibleRectangles);
        }

        [Test]
        public void RectanglesAreIntersected_ShouldReturnFalse_WhenRectanglesAreNotIntersected()
        {
            var firstRectangle = new Rectangle(new Point(3, 3), new Size(3, 2));
            var secondRectangle = new Rectangle(new Point(0, 0), new Size(3, 2));

            RectangleUtils
                .RectanglesAreIntersected(firstRectangle, secondRectangle)
                .Should()
                .BeFalse();
        }

        [Test]
        public void RectanglesAreIntersected_ShouldReturnTrue_WhenRectanglesAreIntersectedInTheCorner()
        {
            var firstRectangle = new Rectangle(new Point(1, 1), new Size(3, 2));
            var secondRectangle = new Rectangle(new Point(0, 0), new Size(3, 2));

            RectangleUtils
                .RectanglesAreIntersected(firstRectangle, secondRectangle)
                .Should()
                .BeTrue();
        }

        [Test]
        public void RectanglesAreIntersected_ShouldReturnTrue_WhenOneInsideTheOther()
        {
            var firstRectangle = new Rectangle(new Point(1, 1), new Size(1, 1));
            var secondRectangle = new Rectangle(new Point(0, 0), new Size(3, 3));

            RectangleUtils
                .RectanglesAreIntersected(firstRectangle, secondRectangle)
                .Should()
                .BeTrue();
        }

        [Test]
        public void RectanglesAreIntersected_ShouldReturnTrue_WhenTheyPlacedAsACross()
        {
            var firstRectangle = new Rectangle(new Point(1, 0), new Size(1, 3));
            var secondRectangle = new Rectangle(new Point(0, 1), new Size(3, 1));

            RectangleUtils
                .RectanglesAreIntersected(firstRectangle, secondRectangle)
                .Should()
                .BeTrue();
        }

        [Test]
        public void GetRectanglesThatCloserToPoint_ShouldBeEmpty_WhenPointInRectangle()
        {
            var point = new Point(1, 1);
            var rectangle = new Rectangle(new Point(0, 0), new Size(3, 2));

            var result = RectangleUtils.GetRectanglesThatCloserToPoint(point, rectangle, 1);

            result.Should().BeEmpty();
        }

        [Test]
        public void GetRectanglesThatCloserToPoint_ShouldBeEmpty_WhenPointOnRectangleSide()
        {
            var point = new Point(2, 0);
            var rectangle = new Rectangle(new Point(0, 0), new Size(3, 2));

            var result = RectangleUtils.GetRectanglesThatCloserToPoint(point, rectangle, 1);

            result.Should().BeEmpty();
        }

        [Test]
        public void GetRectanglesThatCloserToPoint_ShouldNotReturnTheSameRectangle_WhenIntersectsPointCoordinate()
        {
            var point = new Point(0, 0);
            var size = new Size(2, 1);
            var rectangle = new Rectangle(new Point(0, 2), size);
            var expectedRectangle = new Rectangle(new Point(0, 1), size);

            var result = RectangleUtils.GetRectanglesThatCloserToPoint(point, rectangle, 1);

            result.Should().OnlyContain(r => r == expectedRectangle);
        }

        [TestCase(2, 2, 1, 1, TestName = "I quadrant")]
        [TestCase(-5, 2, -4, 1, TestName = "II quadrant")]
        [TestCase(-5, -4, -4, -3, TestName = "III quadrant")]
        [TestCase(2, -4, 1, -3, TestName = "IV quadrant")]
        public void GetRectanglesThatCloserToPoint_ShouldReturnCloserRectangles_OnRectangle(
            int x, int y, int dx, int dy)
        {
            var point = new Point(0, 0);
            var size = new Size(3, 2);
            var rectangle = new Rectangle(new Point(x, y), size);
            var expectedRectangles = new[]
            {
                new Rectangle(new Point(dx, y), size),
                new Rectangle(new Point(x, dy), size),
                new Rectangle(new Point(dx, dy), size),
            };

            var result = RectangleUtils.GetRectanglesThatCloserToPoint(point, rectangle, 1);

            result.Should().BeEquivalentTo(expectedRectangles);
        }

        [TestCase(3, 4, 5)]
        [TestCase(5, 12, 13)]
        [TestCase(8, 15, 17)]
        public void GetRectangleDiagonal_ShouldReturnDiagonal_OnRectangle(
            int width, int height, double expectedResult)
        {
            var rectangle = new Rectangle(new Point(0, 0), new Size(width, height));

            var diagonal = RectangleUtils.GetRectangleDiagonal(rectangle);

            diagonal.Should().BeApproximately(expectedResult, 1e-5);
        }

        [Test]
        public void GetClosestRectangleThatDoesNotIntersectWithOthers_ShouldReturnClosestPossibleRectangle()
        {
            var possibleLocation = new Point(-5, 5);
            var size = new Size(3, 2);
            var center = new Point(0, 0);
            var expectedResult = new Rectangle(new Point(-3, 0), size);

            var result = RectangleUtils.GetClosestRectangleThatDoesNotIntersectWithOthers(
                possibleLocation, size, center, new List<Rectangle>());

            result.Should().Be(expectedResult);
        }

        [Test]
        public void GetClosestRectangleThatDoesNotIntersectWithOthers_ShouldBeNull_WhenAllPossibleRectanglesIntersected()
        {
            var possibleLocation = new Point(0, 1);
            var size = new Size(3, 2);
            var center = new Point(0, 0);
            var rectangles = new List<Rectangle>
            {
                new Rectangle(new Point(0, -1), new Size(7, 1)),
                new Rectangle(new Point(-6, 1), new Size(5, 3)),
                new Rectangle(new Point(-4, -2), new Size(3, 2)),
                new Rectangle(new Point(1, 1), new Size(3, 1))
            };

            var result = RectangleUtils.GetClosestRectangleThatDoesNotIntersectWithOthers(
                    possibleLocation, size, center, rectangles);

            result.Should().BeNull();
        }

        [Test]
        public void GetClosestRectangleThatDoesNotIntersectWithOthers_ShouldGetClosestNotIntersectedRectangle()
        {
            var possibleLocation = new Point(2, 4);
            var size = new Size(3, 2);
            var center = new Point(0, 0);
            var rectangles = new List<Rectangle>
            {
                new Rectangle(new Point(0, -1), new Size(7, 1)),
                new Rectangle(new Point(-6, 1), new Size(5, 3)),
                new Rectangle(new Point(-4, -2), new Size(3, 2)),
                new Rectangle(new Point(1, 1), new Size(3, 1))
            };
            var expectedRectangle = new Rectangle(new Point(0, 2), size);

            var result = RectangleUtils.GetClosestRectangleThatDoesNotIntersectWithOthers(
                possibleLocation, size, center, rectangles);

            result.Should().Be(expectedRectangle);
        }
    }
}
