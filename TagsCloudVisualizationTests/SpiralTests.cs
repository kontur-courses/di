using NUnit.Framework;
using System;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using TagsCloudVisualization;
using FluentAssertions.Execution;
using TagsCloudVisualization.Curves;

namespace TagsCloudVisualizationTests
{
    [TestFixture]
    public class SpiralTests
    {
        private static readonly Point Center = new(100, 100);
        private static readonly double Step = 2;
        private Spiral spiral;

        [SetUp]
        public void SetUp()
        {
            spiral = new Spiral(Center, Math.PI / 180, Step);
        }

        [Test]
        public void Enumerator_ShouldReturnsCenter_WhenFirstCall()
        {
            spiral.First().Should().Be(Center);
        }

        [Test]
        public void Enumerator_AngleShouldBePi_WhenOppositePoint()
        {
            var points = spiral.Take((int)1e5).ToArray();
            using (new AssertionScope())
            {
                for (int i = 360; i < points.Length; i += 1)
                {
                    var opposite = GetAngleFromPoint(points[i - 180]);
                    var currentPoint = GetAngleFromPoint(points[i]);
                    Math.Abs(currentPoint - opposite).Should().BeInRange(Math.PI - 0.2, Math.PI + 0.2);
                }
            }
        }

        [Test]
        public void Enumerator_SpiralRadiusGrows_WhenIteratingWithBigDistance()
        {
            var points = spiral.Take((int)1e5).ToArray();

            using (new AssertionScope())
            {
                for (int i = 100; i < points.Length; i += 1)
                {
                    var squareRadius1 = Center.GetDistanceSquareTo(points[i - 100]);
                    var squareRadius2 = Center.GetDistanceSquareTo(points[i]);
                    squareRadius1.Should().BeLessThan(squareRadius2);
                }
            }

        }

        [Test, Timeout(1000)]
        public void Enumerator_SpiralShouldBeFast_WhenGeneratingBigNumberOfPoints()
        {
            _ = spiral.Take((int)1e5).ToArray();
        }

        private double GetAngleFromPoint(Point point)
            => Math.Atan2(point.Y - Center.Y, point.X - Center.X);
    }
}
