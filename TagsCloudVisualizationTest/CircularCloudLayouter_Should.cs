using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using TagsCloudVisualization.CloudGenerating;
using TagsCloudVisualization.Utils;

namespace TagsCloudVisualizationTest
{
    class CircularCloudLayouter_Should
    {
        private Point center;
        private CircularCloudLayouter layouter;

        [SetUp]
        public void SetUp()
        {
            center = new Point(0, 0);
            layouter = new CircularCloudLayouter(
                new ArchimedeanSpiralGenerator(center, 1, (float)(1 / (180 * Math.PI))));
        }

        [Test]
        public void ReturnRectangleWithSameSize()
        {
            var rectangleSize = new Size(10, 10);
            var placedRectangle = layouter.PutNextRectangle(rectangleSize);
            Assert.AreEqual(rectangleSize, placedRectangle.Size);
        }

        [Test]
        public void PlaceFirstRectangleCenterNearToCenter()
        {
            var rectangleSize = new Size(9, 9);
            var placedRectangle = layouter.PutNextRectangle(rectangleSize);
            var rectangleCenter = placedRectangle.GetCenter();
            
            Assert.That(Math.Abs(center.X - rectangleCenter.X), Is.LessThanOrEqualTo(1));
            Assert.That(Math.Abs(center.Y - rectangleCenter.Y), Is.LessThanOrEqualTo(1));
        }

        [Test]
        public void PlaceTwoRectangles_SoThatTheyDoNotIntersect()
        {
            var rectangleSize = new Size(10, 20);
            var firstPlacedRectangle = layouter.PutNextRectangle(rectangleSize);
            var secondPlacedRectangle = layouter.PutNextRectangle(rectangleSize);

            Assert.False(firstPlacedRectangle.IntersectsWith(secondPlacedRectangle));
        }

        [Test]
        public void PlaceManyRectangles_SoThatTheyDoNotIntersect()
        {
            var rectangleSize = new Size(10, 20);
            var placedRectangles = new List<Rectangle>();
            for (int i = 0; i < 50; i++)
            {
                var nextRectangle = layouter.PutNextRectangle(rectangleSize);
                Assert.False(nextRectangle.IntersectsWithAny(placedRectangles));
                placedRectangles.Add(nextRectangle);
            }
        }

        [Test]
        public void PlaceRectanglesWithGoodDensity()
        {
            const double goodDensityThreshold = 0.6;
            var rectangleSize = new Size(10, 20);
            var placedRectangles = new List<Rectangle>();
            for (int i = 0; i < 50; i++)
            {
                var nextRectangle = layouter.PutNextRectangle(rectangleSize);
                placedRectangles.Add(nextRectangle);
            }

            Assert.That(CalculateDensity(placedRectangles), Is.GreaterThan(goodDensityThreshold));
        }

        private double CalculateDensity(List<Rectangle> rectangles)
        {
            var areaSum = rectangles.Sum(r => r.Size.Width * r.Size.Height);
            var topBorder = rectangles.Min(r => r.Top);
            var bottomBorder = rectangles.Max(r => r.Bottom);
            var rightBorder = rectangles.Max(r => r.Right);
            var leftBorder = rectangles.Min(r => r.Left);

            var bigRectangleSize = (rightBorder - leftBorder) * (bottomBorder - topBorder);
            return (double) areaSum / bigRectangleSize;
        }
    }
}
