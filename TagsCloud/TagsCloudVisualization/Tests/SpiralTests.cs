using System;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Layouters;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class SpiralTests
    {
        private Spiral spiral;
        private PointF spiralCenter = new PointF(500, 500);

        [SetUp]
        public void Init() => spiral = new Spiral(spiralCenter, 1, 1);

        [TestCase(0, 10, TestName = "radius is zero")]
        [TestCase(10, 0, TestName = "increment is zero")]
        [TestCase(0, 0, TestName = "radius and increment are zero")]
        public void Constructor_Should_ThrowArgumentException_When(float radius, float increment)
        {
            Action action = () => new Spiral(new Point(1, 1), radius, increment);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void FirstCoordinateEqualToCenter()
        {
            var coordinates = spiral.GetEnumerator().Current;

            coordinates.Should().BeEquivalentTo(spiralCenter);
        }

        [Test]
        public void MoveNextIncrementsCoordinates()
        {
            var coordinatesArray = spiral.Take(2).ToArray();

            coordinatesArray[0].Should().NotBeEquivalentTo(coordinatesArray[1]);
        }

        [Test]
        public void SpiralStopsReturningCoordinates_WhenSpiralParametersOverflow()
        {
            var coordinatesCollection = new Spiral(new Point(100, 100), 1, 1, float.MaxValue).Take(1);

            coordinatesCollection.Should().BeEmpty();
        }
    }
}