using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using System.Drawing;
using TagsCloudVisualization;

namespace TagsCloudVisualization_Tests
{
    [TestFixture]
    public class CircularCloudLayouter_Should
    {
        private CircularCloudLayouter cloudLayouter;
        private Point center;
        private Spiral spiral;
        private Size defaultSize;
        private List<Rectangle> rectangles;

        [SetUp]
        public void SetUp()
        {
            center = new Point(0,0);
            spiral = new Spiral(center, 0.0005, 0);
            cloudLayouter = new CircularCloudLayouter(spiral);
            defaultSize = new Size(200, 100);
            rectangles = new List<Rectangle>();
        }

        [Test]
        public void PutNextRectangle_ThrowArgumentException_OnInvalidSize()
        {
            var size = new Size(-1, -1);
            cloudLayouter.Invoking(obj => obj.PutNextRectangle(size)).Should().Throw<ArgumentException>().WithMessage("*?positive");
        }

        [Test]
        public void PutNextRectangle_PutSingleRectangleInCenter_SingleRectangleCenterShifted()
        {
            var expectedRectangle = new Rectangle(new Point(-100, -50), defaultSize);
            cloudLayouter.PutNextRectangle(defaultSize).Should().BeEquivalentTo(expectedRectangle);
        }

        [Test]
        public void PutNextRectangle_PutTwoRectangles_RectanglesDoNotIntersect()
        {
            var random = new Random();
            rectangles.Add(cloudLayouter.PutNextRectangle(new Size(random.Next(1, 200), random.Next(1, 200))));
            cloudLayouter.PutNextRectangle(defaultSize).IntersectsWith(rectangles[0]).Should().BeFalse();
        }

        [Test]
        public void PutNextRectangle_PutSeveralRectangles_RectanglesDoNotIntersect()
        {
            var random = new Random();
            for (var i = 0; i < 500; i++)
            {
                var nextRectangle = cloudLayouter.PutNextRectangle(new Size(random.Next(1, 200), random.Next(1, 200)));
                rectangles.Any(nextRectangle.IntersectsWith).Should().BeFalse();
                rectangles.Add(nextRectangle);
            }
        }

        [Test]
        public void PutNextRectangle_PutsRectanglesTightEnough()
        {
            var totalCloudArea = 0;
            var random = new Random();
            for (var i = 0; i < 500; i++)
            {
                var nextRectangle = cloudLayouter.PutNextRectangle(new Size(random.Next(1, 200), random.Next(1, 200)));
                totalCloudArea += nextRectangle.Width * nextRectangle.Height;
                rectangles.Add(nextRectangle);
            }

            var totalCircleCloudRadius = GetSurroundingCircleRadius();
            var totalCircleCloudArea = Math.PI * Math.Pow(totalCircleCloudRadius, 2);
            totalCircleCloudArea.Should().BeGreaterOrEqualTo(totalCloudArea);
        }
        
        private int GetSurroundingCircleRadius()
        {
            if (rectangles.Count == 0) return 0;
            return rectangles
                .Select(rect => new Point(Helper.MaxSignedAbs(rect.Left, rect.Right),
                    Helper.MaxSignedAbs(rect.Top, rect.Bottom)))
                .Select(point => point.GetDistanceTo(center)).Max();
        }
    }
}
