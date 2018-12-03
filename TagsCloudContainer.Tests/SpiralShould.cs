using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudContainer.Tests
{
    [TestFixture]
    class Spiral_Should
    {
        private Spiral spiral;
        private Point center;

        [SetUp]
        public void SetUp()
        {
            center = new Point(100, 100);
            spiral = new Spiral(center);
        }

        [Test]
        public void ReturnCenterPoint_OnFirstInvocation()
        {
            var firstPoint = spiral.GetNextPoint();

            firstPoint.Should().BeEquivalentTo(center);
        }

        [Test]
        public void IncreaseSpiralAngle_AfterGetNextPointInvocation()
        {
            var spiralPointInitialValue = spiral.GetCurrentSpiralAngle();
            spiral.GetNextPoint();

            spiral.GetCurrentSpiralAngle().Should().BeGreaterThan(spiralPointInitialValue);
        }
    }
}
