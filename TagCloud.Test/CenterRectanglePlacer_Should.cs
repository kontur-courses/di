using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Extensions;
using TagCloud.RectanglePlacer;

namespace TagCloud.Tests
{
    [TestFixture]
    public class CenterRectanglePlacer_Should
    {
        [TestCase(0, 0, TestName = "Then point is (0,0)")]
        [TestCase(10, 10, TestName = "Then point is (10,10)")]
        [TestCase(-10, -10, TestName = "Then point is (-10,-10)")]
        public void PlaceRectangleWithCenterInPoint(int x, int y)
        {
            var point = new Point(x, y);
            var rectanglePlacer = new CenterRectanglePlacer();

            var rectangleCenter = rectanglePlacer
                .PlaceRectangle(new Size(10, 10), point)
                .GetCenter();

            rectangleCenter.Should().Be(point);
        }
    }
}