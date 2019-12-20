using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CloudLayouter;
using CloudLayouter.Spiral;
using FluentAssertions;
using NUnit.Framework;

namespace CloudLayouterеTests
{
    public class CircularCloudLayouterTests
    {
        private List<Rectangle> rectangles;

        [SetUp]
        public void SetUp()
        {
            rectangles = new List<Rectangle>();
        }

        [Test]
        public void Constructor_DoesNotThrow_WithСorrectСenter([ValueSource(nameof(cloudCenters))] Point center)
        {
            Action action = () => new CircularCloudLayouter(new CircularSpiral());
            action.Should().NotThrow();
        }


        [Test]
        public void PutNextRectangle_LocateFirstRectangle_OnSpecifiedByXCenter(
            [ValueSource(nameof(cloudCenters))] Point center)
        {
            var circularCloudLayouter = new CircularCloudLayouter(new CircularSpiral());
            circularCloudLayouter.SetCenter(center);
            rectangles.Add(circularCloudLayouter.PutNextRectangle(new Size(31, 42)));
            rectangles[0].X.Should().Be(center.X - 31 / 2);
        }

        [Test]
        public void PutNextRectangle_LocateFirstRectangle_OnSpecifiedByYCenter(
            [ValueSource(nameof(cloudCenters))] Point center)
        {
            var circularCloudLayouter = new CircularCloudLayouter(new CircularSpiral());
            circularCloudLayouter.SetCenter(center);
            rectangles.Add(circularCloudLayouter.PutNextRectangle(new Size(31, 42)));
            rectangles[0].Y.Should().Be(center.Y - 42 / 2);
        }


        [TestCase(2, TestName = "TwoRectangles")]
        [TestCase(10, TestName = "TenRectangles")]
        [TestCase(20, TestName = "TwentyRectangles")]
        [TestCase(100, TestName = "HundredRectangles")]
        public void PutNextRectangle_RectanglesMustNotIntersect(int countRectangles)
        {
            var circularCloudLayouter = new CircularCloudLayouter(new CircularSpiral());
            circularCloudLayouter.SetCenter(new Point(0, 0));
            rectangles.AddRange(Enumerable.Range(10, countRectangles)
                .Select(i => circularCloudLayouter.PutNextRectangle(new Size(i * 3, i))));

            CircularCloudLayouter.HasOverlappingRectangles(rectangles).Should().BeFalse();
        }

        private static IEnumerable<Point> cloudCenters = Enumerable
            .Range(-1, 3)
            .SelectMany(i => Enumerable
                .Range(-1, 3)
                .Select(j => new Point(i, j)));
    }
}