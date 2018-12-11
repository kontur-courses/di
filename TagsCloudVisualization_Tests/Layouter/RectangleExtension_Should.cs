using NUnit.Framework;
using System.Drawing;
using FluentAssertions;
using TagsCloudVisualization.Layouter;

namespace TagsCloudVisualization_Tests
{
    [TestFixture]
    public class RectangleExtension_Should
    {
        [Test]
        public void ShiftRectangleToTopLeftCorner_ReturnShiftedRectangle()
        {
            var defaultSize = new Size(200, 100);
            var actualRectangle = new Rectangle(new Point(0, 0), defaultSize);
            var expectedRectangle = new Rectangle(new Point(-100, -50), defaultSize);
            actualRectangle.ShiftRectangleToTopLeftCorner().Should().BeEquivalentTo(expectedRectangle);
        }

        [Test]
        public void ShiftRectangleToBitMapCenter_ReturnCorrectlyShiftedRectangle()
        {
            var defaultSize = new Size(200, 100);
            var bitmap = new Bitmap(500, 500);
            var actualRectangle = new Rectangle(new Point(0, 0), defaultSize);
            var expectedRectangle = new Rectangle(new Point(250, 250), defaultSize);
            actualRectangle.ShiftRectangleToBitMapCenter(bitmap).Should().BeEquivalentTo(expectedRectangle);
        }
    }
}