using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudContainer.TagsCloudVisualization
{
    public class RectangleExtensionsTests
    {
        [Test]
        public void IntersectsWithRectangles_ReturnsFalse_WhenListIsNull()
        {
            Rectangle.Empty.IntersectsWithRectangles(null).Should().Be(false);
        }

        [Test]
        public void IntersectsWithRectangles_ReturnsFalse_WhenListIsEmpty()
        {
            Rectangle.Empty.IntersectsWithRectangles(new List<Rectangle>()).Should().Be(false);
        }

        [Test]
        public void IntersectsWithRectangles_ReturnsTrue_WhenIntersectsWithRectangleFromList()
        {
            var rectangles = new List<Rectangle> {new Rectangle(Point.Empty, new Size(100, 100))};
            var rectangle = new Rectangle(new Point(50, 50), new Size(100, 100));

            rectangle.IntersectsWithRectangles(rectangles).Should().Be(true);
        }

        [Test]
        public void IntersectsWithRectangles_ReturnsTrue_WhenRectangleIsTheSameAsInList()
        {
            var rectangle = new Rectangle(new Point(50, 50), new Size(100, 100));
            var rectangles = new List<Rectangle> {rectangle};

            rectangle.IntersectsWithRectangles(rectangles).Should().Be(true);
        }

        [Test]
        public void IntersectsWithRectangles_ReturnsFalse_WhenRectangleNotIntersectsWithRectanglesFromList()
        {
            var rectangles = new List<Rectangle> {new Rectangle(Point.Empty, new Size(100, 100))};
            var rectangle = new Rectangle(new Point(150, 150), new Size(100, 100));

            rectangle.IntersectsWithRectangles(rectangles).Should().Be(false);
        }
    }
}