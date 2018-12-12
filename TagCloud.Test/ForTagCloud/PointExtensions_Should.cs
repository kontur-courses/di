using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Extensions;

namespace TagCloud.Tests.ForTagCloud
{
    [TestFixture]
    class PointExtensions_Should
    {
        [Test]
        public void GetCorrectBounds()
        {
            var points = new[] { new Point(0, 0), new Point(10, 10), new Point(-10, -10) };
            var expectedLocation = new Point(-10, -10);
            var expectedSize = new Size(21, 21);

            var bounds = points.GetBounds();

            bounds.Should().Be(new Rectangle(expectedLocation, expectedSize));
        }
    }
}
