using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Algorithms;
using FakeItEasy;
using TagsCloudContainer.Settings;

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
            var settings = A.Fake<ICloudSettings>();
            center = new Point(100, 100);
            archimedeanSpiral = new ArchimedeanSpiral(settings);
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
