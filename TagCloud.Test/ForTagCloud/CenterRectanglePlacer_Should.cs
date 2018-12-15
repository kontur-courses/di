using System.Drawing;
using Autofac;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.RectanglePlacer;

namespace TagCloud.Tests.ForTagCloud
{
    [TestFixture]
    public class CenterRectanglePlacer_Should : TestBase
    {
        [TestCase(0, 0, TestName = "Then point is (0,0)")]
        [TestCase(10, 10, TestName = "Then point is (10,10)")]
        [TestCase(-10, -10, TestName = "Then point is (-10,-10)")]
        public void PlaceRectangleWithCenterInPoint(int x, int y)
        {
            var point = new Point(x, y);
            var rectanglePlacer = container.Resolve<CenterRectanglePlacer>();

            var rectangle = rectanglePlacer
                .PlaceRectangle(new Size(10, 10), point);

            rectangle.Location.Should().Be(new Point(point.X - 5, point.Y - 5));
        }
    }
}