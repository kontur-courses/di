using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.RectangleGenerator.PointGenerator;

namespace TagsCloudContainer.Tests.RectangleGenerator
{
    class SpiralGenerator_Should
    {
        private static Point center;
        private static SpiralGenerator generator;

        [SetUp]
        public void SetUp()
        {
            center = new Point(10, 10);
            generator = new SpiralGenerator(center);
        }

        [Test]
        public void GetNextPoint_WhenGetOnePoint_ReturnCenterPoint()
        {
            generator.GetNextPoint().Should().Be(generator.Center);
        }

        [Test]
        public void GetNextPoint_WhenGetPoints_ReturnDifferentPoints()
        {
            var hash = new HashSet<Point>();
            var point = new Point();
            while (point.X>=0 && point.Y >=0)
            {
                point = generator.GetNextPoint();
                hash.Should().NotContain(point);
                hash.Add(point);
            }
        }
    }
}
