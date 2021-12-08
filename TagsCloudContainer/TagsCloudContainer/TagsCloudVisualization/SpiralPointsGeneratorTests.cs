using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudContainer.TagsCloudVisualization
{
    public class SpiralPointsGeneratorTests
    {
        [Test]
        public void Constructor_Throws_WhenCenterCoordinatesAreNegative()
        {
            Action act = () => new SpiralPointsGenerator(new Point(-1, -1), 0, 0, 1, 1);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Constructor_Throws_WhenStartRadiusIsNegative()
        {
            Action act = () => new SpiralPointsGenerator(new Point(1, 1), -1, 0, 1, 1);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Constructor_Throws_WhenAngleDeltaIsZero()
        {
            Action act = () => new SpiralPointsGenerator(new Point(1, 1), 1, 0, 0, 1);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Constructor_Throws_WhenRadiusDeltaIsZero()
        {
            Action act = () => new SpiralPointsGenerator(new Point(1, 1), 1, 0, 1, 0);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Constructor_NotThrows_WhenParamsAreValid()
        {
            Action act = () => new SpiralPointsGenerator(new Point(1, 1), 1, 0, 1, 1);

            act.Should().NotThrow<ArgumentException>();
        }

        [Test]
        public void GetSpiralPoints_ReturnsCenter_WhenItIsCalledFirstTime()
        {
            var center = Point.Empty;
            var generator = new SpiralPointsGenerator(center);

            generator.GetSpiralPoints().FirstOrDefault().Should().Be(center);
        }

        [Test]
        public void GetSpiralPoints_ReturnsDifferentPoints()
        {
            var points = new List<Point>();
            var enumerator = new SpiralPointsGenerator(new Point(100, 100), 10, 0, 0.1, 1).GetSpiralPoints()
                .GetEnumerator();

            enumerator.MoveNext();
            for (var i = 0; i < 100; i++)
            {
                enumerator.MoveNext();
                points.Should().NotContain(enumerator.Current);
                points.Add(enumerator.Current);
            }
        }

        [Test]
        public void GetSpiralPoints_ReturnsPoints_WithIncreasingDistanceToCenter()
        {
            var distances = new List<double>();
            var center = new Point(100, 100);
            var enumerator = new SpiralPointsGenerator(center, 10, 0, 0.1, 1).GetSpiralPoints().GetEnumerator();

            enumerator.MoveNext();
            for (var i = 0; i < 100; i++)
            {
                enumerator.MoveNext();
                var point = enumerator.Current;
                var distance = Math.Sqrt((center.X - point.X) * (center.X - point.X) +
                                         (center.Y - point.Y) * (center.Y - point.Y));
                distances.Should().NotContain(distance);
                distances.Add(distance);
            }
        }
    }
}