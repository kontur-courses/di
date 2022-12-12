using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.App.Layouter;

namespace TagsCloudContainerTests
{
    public class RectangleExtensions_Should
    {
        [Test]
        public void AreIntersected_ShouldReturnTrue_WhenRectangleHaveIntersectedRectangle()
        {
            var rect = new Rectangle(50, 50, 50, 50);
            var rectangles = new List<Rectangle>();
            rectangles.Add(new Rectangle(0, 50, 49, 50));
            rectangles.Add(new Rectangle(50, 101, 50, 50));
            rectangles.Add(new Rectangle(52, 52, 48, 48));
            rectangles.Add(new Rectangle(50, 0, 50, 49));
            rect.AreIntersected(rectangles).Should().BeTrue();
        }

        [Test]
        public void AreIntersected_ShouldReturnFalse_WhenRectangleNotIntersectedWithRectangles()
        {
            var rect = new Rectangle(50, 50, 50, 50);
            var rectangles = new List<Rectangle>();
            rectangles.Add(new Rectangle(101, 50, 50, 50));
            rectangles.Add(new Rectangle(0, 50, 49, 50));
            rectangles.Add(new Rectangle(50, 101, 50, 50));
            rectangles.Add(new Rectangle(50, 0, 50, 49));
            rect.AreIntersected(rectangles).Should().BeFalse();
        }

        [Test]
        public void AreIntersected_ShouldReturnTrue_WhenHaveIntersectedRectanglesInList()
        {
            var rectangles = new List<Rectangle>();
            rectangles.Add(new Rectangle(50, 50, 50, 50));
            rectangles.Add(new Rectangle(0, 50, 49, 50));
            rectangles.Add(new Rectangle(50, 101, 50, 50));
            rectangles.Add(new Rectangle(52, 52, 48, 48));
            rectangles.Add(new Rectangle(50, 0, 50, 49));
            rectangles.AreIntersected().Should().BeTrue();
        }

        [Test]
        public void AreIntersected_ShouldReturnFalse_WhenNotIntersectedRectanglesInList()
        {
            var rectangles = new List<Rectangle>();
            rectangles.Add(new Rectangle(101, 50, 50, 50));
            rectangles.Add(new Rectangle(0, 50, 49, 50));
            rectangles.Add(new Rectangle(50, 101, 50, 50));
            rectangles.Add(new Rectangle(50, 0, 50, 49));
            rectangles.AreIntersected().Should().BeFalse();
        }
    }
}
