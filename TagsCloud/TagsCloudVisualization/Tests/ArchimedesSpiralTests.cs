using System;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class ArchimedesSpiralTests
    {
        private ArchimedesSpiral archimedesSpiral;
        private Point spiralCenter = new Point(500, 500);

        [SetUp]
        public void Init() => archimedesSpiral = new ArchimedesSpiral(spiralCenter, 1, 1);

        [TestCase(0, 10, TestName = "radius is zero")]
        [TestCase(10, 0, TestName = "increment is zero")]
        [TestCase(0, 0, TestName = "radius and increment are zero")]
        public void Constructor_Should_ThrowArgumentException_When(float radius, float increment)
        {
            Action action = () => new ArchimedesSpiral(new Point(1, 1), radius, increment);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void FirstCoordinateEqualToCenter()
        {
            var coordinates = archimedesSpiral.GetEnumerator().Current;

            coordinates.Should().BeEquivalentTo(spiralCenter);
        }

        [Test]
        public void MoveNextIncrementsCoordinates()
        {
            var coordinatesArray = archimedesSpiral.Take(2).ToArray();

            coordinatesArray[0].Should().NotBeEquivalentTo(coordinatesArray[1]);
        }

        [Test]
        public void SpiralStopsReturningCoordinates_WhenSpiralParametersOverflow()
        {
            var coordinatesCollection = new ArchimedesSpiral(new Point(100, 100), 1, 1, float.MaxValue).Take(1);

            coordinatesCollection.Should().BeEmpty();
        }
    }
}