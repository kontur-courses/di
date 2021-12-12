using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using App.Implementation.GeometryUtils;
using FluentAssertions;
using NUnit.Framework;

namespace TagCloudContainerTaskTests.GeometryTests
{
    public class RectangleExtensionTests
    {
        private Rectangle rect;

        [SetUp]
        public void SetUp()
        {
            rect = new Rectangle(Point.Empty, new Size(10, 10));
        }

        [Test]
        public void IntersectWithAny_ShouldDetectIntersection()
        {
            var rectangles = Rectangles().ToList();

            rect.IntersectsWithAny(rectangles).Should().BeTrue();
        }

        [Test]
        public void IntersectWithAny_ShouldThrowOnNull()
        {
            Action act = () => rect.IntersectsWithAny(null);

            act
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("rectangles list can't be null");
        }

        [Test]
        public void IntersectWithAny_ShouldDontThrowOnZeroCount()
        {
            Action act = () => rect.IntersectsWithAny(new List<Rectangle>());

            act.Should().NotThrow();
        }

        [Test]
        public void GetCorners_ShouldReturnRectangleCorners(
            [ValueSource(nameof(Rectangles))] Rectangle rect)

        {
            var expectedCorners = new List<Point>
            {
                new Point(rect.X, rect.Y),
                new Point(rect.X + rect.Width, rect.Y),
                new Point(rect.X, rect.Y + rect.Height),
                new Point(rect.X + rect.Width, rect.Y + rect.Height)
            };

            var actualCorners = rect.GetCorners().ToList();

            actualCorners.Count.Should().Be(expectedCorners.Count);
            actualCorners.Any(corner => expectedCorners.Contains(corner)).Should().BeTrue();
        }

        private static IEnumerable<Rectangle> Rectangles()
        {
            yield return new Rectangle(Point.Empty, new Size(5, 5));
            yield return new Rectangle(new Point(5, 0), new Size(10, 5));
            yield return new Rectangle(new Point(0, 5), new Size(7, 7));
        }
    }
}