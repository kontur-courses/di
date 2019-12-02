using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Geometry;
using TagsCloudVisualization.TagCloudLayouters;

namespace TagCloudVisualizationTests.Geometry
{
    [TestFixture]
    class CenterMassCheckerShould
    {
        [Test]
        public void ReturnRectangleCenter_When_OneRectangleAdded()
        {
            var centre = new Point(0, 0);
            var tagCloud = new CircularCloudLayouter(centre, 200);
            var rectangles = new List<Rectangle>() { tagCloud.PutNextRectangle(new Size(2, 2)) };
            CenterMassChecker.FindCenterMass(rectangles).Should().Be(new PointF(0, 0));
        }

        [Test]
        public void ReturnCenter_When_FourSameRectanglesAdded()
        {
            var centre = new Point(0, 0);
            var tagCloud = new CircularCloudLayouter(centre, 200);
            var rectangles = Enumerable.Range(0, 9).Select(b => tagCloud.PutNextRectangle(new Size(2, 2))).ToList();
            CenterMassChecker.FindCenterMass(rectangles).Should().Be(new PointF(0, 0));
        } 
    }
}
