using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Algorithm.SpiralBasedLayouter;

namespace TagCloudTests.Algorithm.SpiralBasedLayouter
{
    public class ShiftUtilsTests
    {
        [Test]
        public void GetShiftToTheFirstQuadrant_ShouldReturnZeroShift_WhenAllRectanglesInFirstQuadrant()
        {
            var center = new Point(0, 0);
            var rectangles = new List<Rectangle>
            {
                new Rectangle(new Point(0, 5), new Size(7, 1)),
                new Rectangle(new Point(1, 3), new Size(3, 1)),
                new Rectangle(new Point(1, 1), new Size(3, 1))
            };
            var expectedShift = new Point(0, 0);

            ShiftUtils.GetShiftToTheFirstQuadrant(center, rectangles).Should().Be(expectedShift);
        }

        [Test]
        public void GetShiftToTheFirstQuadrant_ShouldReturnShiftOnlyByX_WhenRectanglesInFirstAndSecondQuadrant()
        {
            var center = new Point(0, 0);
            var rectangles = new List<Rectangle>
            {
                new Rectangle(new Point(0, 5), new Size(7, 1)),
                new Rectangle(new Point(1, 3), new Size(3, 1)),
                new Rectangle(new Point(1, 1), new Size(3, 1)),
                new Rectangle(new Point(-6, 1), new Size(5, 3))
            };
            var expectedShift = new Point(6, 0);

            ShiftUtils.GetShiftToTheFirstQuadrant(center, rectangles).Should().Be(expectedShift);
        }

        [Test]
        public void GetShiftToTheFirstQuadrant_ShouldReturnShiftOnlyByY_WhenRectanglesInFirstAndForthQuadrant()
        {
            var center = new Point(0, 0);
            var rectangles = new List<Rectangle>
            {
                new Rectangle(new Point(0, 5), new Size(7, 1)),
                new Rectangle(new Point(1, 3), new Size(3, 1)),
                new Rectangle(new Point(1, 1), new Size(3, 1)),
                new Rectangle(new Point(1, -3), new Size(3, 1)),
            };
            var expectedShift = new Point(0, 3);

            ShiftUtils.GetShiftToTheFirstQuadrant(center, rectangles).Should().Be(expectedShift);
        }

        [Test]
        public void GetShiftToTheFirstQuadrant_ShouldReturnShiftByXandY_WhenRectanglesInAllQuadrants()
        {
            var center = new Point(0, 0);
            var rectangles = new List < Rectangle >
            {
                new Rectangle(new Point(0, -1), new Size(7, 1)),
                new Rectangle(new Point(-6, 1), new Size(5, 3)),
                new Rectangle(new Point(-4, -2), new Size(3, 2)),
                new Rectangle(new Point(1, -3), new Size(3, 1)),
                new Rectangle(new Point(1, 1), new Size(3, 1))
            };
            var expectedShift = new Point(6, 3);

            ShiftUtils.GetShiftToTheFirstQuadrant(center, rectangles).Should().Be(expectedShift);
        }

        [Test]
        public void ShiftPoint_ShouldReturnShiftedPoint_OnPoint()
        {
            var original = new Point(2, 5);
            var shift = new Point(2, 2);
            var expected = new Point(4, 7);

            var result = ShiftUtils.ShiftPoint(original, shift, 1);

            result.Should().Be(expected);
        }

        [Test]
        public void GetShiftedAndResizedRectangle_ShouldReturnShiftedAndIncreasedRectangle()
        {
            var size = new Size(2, 1);
            var originalRectangle = new Rectangle(new Point(1, 2), size);
            var shift = new Point(2, 3);
            var increaseParameter = 4;
            var expectedRectangle = new Rectangle(new Point(12, 20), new Size(8, 4));

            var result = ShiftUtils.GetShiftedAndResizedRectangle(originalRectangle, shift, increaseParameter);

            result.Should().Be(expectedRectangle);
        }
    }
}
