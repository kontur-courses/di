using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.Tests
{
    public class VisualizerTests
    {
        [Test]
        public void GetSizeOfImagePayload_ShouldReturnCorrectSize_OnOneRectangle()
        {
            var size = new Size(3, 4);
            var rectangle = new Rectangle(new Point(0, 0), size);

            var sizeOfImage = Visualizer.GetSizeOfImagePayload(new List<Rectangle> { rectangle });

            sizeOfImage.Should().Be(size);
        }

        [Test]
        public void GetSizeOfImagePayload_ShouldReturnCorrectSize_OnManyRectangles()
        {
            var rectangles = new List<Rectangle>
            {
                new Rectangle(new Point(0, -1), new Size(7, 1)),
                new Rectangle(new Point(-6, 1), new Size(5, 3)),
                new Rectangle(new Point(-4, -2), new Size(3, 2)),
                new Rectangle(new Point(1, -3), new Size(3, 1)),
                new Rectangle(new Point(1, 1), new Size(3, 1))
            };
            var expectedSize = new Size(13, 7);

            var sizeOfImage = Visualizer.GetSizeOfImagePayload(rectangles);

            sizeOfImage.Should().Be(expectedSize);
        }
    }
}
