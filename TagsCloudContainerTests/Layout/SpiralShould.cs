using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Layout;

namespace TagsCloudContainerTests.Layout
{
    [TestFixture]
    public class SpiralShould
    {
        private Spiral spiral;

        [SetUp]
        public void SetUp()
        {
            spiral = new Spiral(new Point(0, 0));
        }

        [Test]
        public void ReturnCenterAsFirstLocation()
        {
            spiral.GetNextLocation().Should().Be(new PointF(0, 0));
        }

        [Test]
        public void NotReturnCenterAfterFirstLocation()
        {
            spiral.GetNextLocation();

            for (var i = 0; i < 100; i++)
                spiral.GetNextLocation().Should().NotBe(new PointF(0, 0));
        }

        [Test]
        public void NotReturnStrangeNumbers()
        {
            for (var i = 0; i < 10; i++)
            {
                var location = spiral.GetNextLocation();

                location.X.Should().BeLessThan(1).And.BeGreaterThan(-1);
                location.Y.Should().BeLessThan(1).And.BeGreaterThan(-1);
            }

        }
    }
}