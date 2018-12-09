using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Algorithms;

namespace TagsCloudContainer.Tests
{
    [TestFixture]
    class Spiral_Should
    {
        private ArchimedeanSpiral archimedeanSpiral;
        private Point center;

        [SetUp]
        public void SetUp()
        {
            center = new Point(100, 100);
            archimedeanSpiral = new ArchimedeanSpiral(center);
        }

        [Test]
        public void ReturnCenterPoint_OnFirstInvocation()
        {
            var firstPoint = archimedeanSpiral.GetNextPoint();

            firstPoint.Should().BeEquivalentTo(center);
        }

        [Test]
        public void IncreaseSpiralAngle_AfterGetNextPointInvocation()
        {
            var spiralPointInitialValue = archimedeanSpiral.GetCurrentSpiralAngle();
            archimedeanSpiral.GetNextPoint();

            archimedeanSpiral.GetCurrentSpiralAngle().Should().BeGreaterThan(spiralPointInitialValue);
        }
    }
}
