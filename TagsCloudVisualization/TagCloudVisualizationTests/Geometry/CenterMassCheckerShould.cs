using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Geometry;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.TagCloudLayouters;

namespace TagCloudVisualizationTests.Geometry
{
    [TestFixture]
    class CenterMassCheckerShould
    {
        private CircularCloudLayouter circularCloudLayouter;

        [SetUp]
        public void SetUp()
        {
            circularCloudLayouter = new CircularCloudLayouter(new CloudSettings(new Point(0, 0), 200));
        }

        [Test]
        public void ReturnRectangleCenter_When_OneRectangleAdded()
        {
            var rectangles = new List<Rectangle>() { circularCloudLayouter.PutNextRectangle(new Size(2, 2)) };
            CenterMassChecker.FindCenterMass(rectangles).Should().Be(new PointF(0, 0));
        }

        [Test]
        public void ReturnCenter_When_FourSameRectanglesAdded()
        {
            var rectangles = Enumerable.Range(0, 9).Select(b => circularCloudLayouter.PutNextRectangle(new Size(2, 2))).ToList();
            CenterMassChecker.FindCenterMass(rectangles).Should().Be(new PointF(0, 0));
        } 
    }
}
