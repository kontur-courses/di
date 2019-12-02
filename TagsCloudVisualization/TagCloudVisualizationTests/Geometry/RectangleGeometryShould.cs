using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Geometry;

namespace TagCloudVisualizationTests.Geometry
{
    [TestFixture]
    class RectangleGeometryTest
    {
        private readonly Size rectangleSize = new Size(1, 4);
        private readonly Point rectangleLocation = new Point(3, 2);

        [Test]
        public void ReturnRightRectangles()
        {
            var expectedRectangles = new List<Rectangle>()
            {
                new Rectangle(3,0,1,4),
                new Rectangle(3, 2, 1, 4),
                new Rectangle(2, 2, 1, 4),
                new Rectangle(3, -2, 1, 4),
                new Rectangle(2,-2, 1, 4)
            };

            RectangleGeometry.GetCornerRectangles(rectangleSize, new HashSet<Point>() { rectangleLocation })
                .ToList()
                .Should()
                .BeEquivalentTo(expectedRectangles);
        }

        [Test]
        public void ReturnRightCorners()
        {
            var expectedPoints = new List<Point>()
            {
                new Point(3, 2),
                new Point(4, 2),
                new Point(3, 6),
                new Point(4, 6)
            };

            new Rectangle(rectangleLocation, rectangleSize).GetCorners()
                .ToList()
                .Should()
                .BeEquivalentTo(expectedPoints);    
        }

        [Test]
        public void ReturnRightSquare()
        {
            var rectangle = new Rectangle(rectangleLocation, rectangleSize);
            rectangle.Square().Should().Be(4);
        }
    }
}
