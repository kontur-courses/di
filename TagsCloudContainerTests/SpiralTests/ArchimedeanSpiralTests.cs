using FluentAssertions;
using NUnit.Framework;
using System.Drawing;
using TagsCloudContainer;

namespace TagsCloudContainerTests
{
    internal class ArchimedeanSpiralTests
    {
        private Point center;
        private ArchimedeanSpiral spiral;

        [SetUp]
        public void SetUp()
        {
            var settings = SettingsProvider.GetSettings();
            center = settings.Center;
            spiral = new ArchimedeanSpiral(settings);
        }

        [Test]
        public void Should_StartAtCenter()
        {
            center.Should().BeEquivalentTo(spiral.GetNext());
        }

        [Test]
        public void Should_IncreaseDistanceFromCenter()
        {
            var spiralPoint = spiral.GetNext();
            var distance = center.GetDistance(spiralPoint);

            for (var i = 1; i <= 1000; i++)
            {
                spiralPoint = spiral.GetNext();
                if (i % 250 == 0)
                {
                    var newDistance = center.GetDistance(spiralPoint);
                    newDistance.Should().BeGreaterThan(distance);
                    distance = newDistance;
                }
            }
        }
    }
}
