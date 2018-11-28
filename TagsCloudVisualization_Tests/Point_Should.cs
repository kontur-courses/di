using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.TagCloudVisualization.Extensions;

namespace TagsCloudVisualization
{

    [TestFixture]
    public class PointExtension_Should
    {
        [Test]
        public void ShiftToLeftRectangleCorner_CorrectShift()
        {
            var centerRectanglePoint = new Point(100, 100);
            var rectangleSize = new Size(80, 40);
            centerRectanglePoint.ShiftToLeftRectangleCorner(rectangleSize)
                .Should().Be(new Point(60, 80));
        }
     
    }
}
