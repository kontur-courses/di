using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudContainer
{
    [TestFixture]
    class CircularTagsCloudLayouterTests
    {
        private CircularTagsCloudLayouter circularCloudLayouter;
        private List<Size> rectangleSizes = new List<Size>();
        private Point cloudCenter;

        [SetUp]
        public void ClearRectangleSizesList()
        {
            rectangleSizes = new List<Size>();
        }


        [Test]
        public void ShouldPutFirstRectangle()
        {
            circularCloudLayouter = new CircularTagsCloudLayouter();
            rectangleSizes.Add(new Size(10, 10));

            var rectangle = circularCloudLayouter.PutNextRectangle(rectangleSizes[0]);

            rectangle.Should().NotBe(null);
        }

        [Test]
        public void ShouldPutFirstRectangleInCenter()
        {
            cloudCenter = new Point(0, 0);
            circularCloudLayouter = new CircularTagsCloudLayouter();
            rectangleSizes.Add(new Size(7, 10));

            var rectangle = circularCloudLayouter.PutNextRectangle(rectangleSizes[0]);

            rectangle.Contains(cloudCenter).Should().BeTrue();
        }

        [Test]
        [TestCase(0, 4)]
        [TestCase(3, -5)]
        [TestCase(-3, -5)]
        [TestCase(-3, 5)]
        public void ThrowsArgumentException_IfNotPositiveSizeOfRectangle(int width, int height)
        {
            circularCloudLayouter = new CircularTagsCloudLayouter();
            rectangleSizes.Add(new Size(width, height));
            Assert.Throws<ArgumentException>(() => circularCloudLayouter.PutNextRectangle(rectangleSizes[0]));
        }

        [Test]
        public void PutTwoRectangles_OnFreePlane()
        {
            circularCloudLayouter = new CircularTagsCloudLayouter();
            rectangleSizes.Add(new Size(10, 20));
            rectangleSizes.Add(new Size(13, 6));

            var rectangle1 = circularCloudLayouter.PutNextRectangle(rectangleSizes[0]);
            var rectangle2 = circularCloudLayouter.PutNextRectangle(rectangleSizes[1]);

            rectangle1.Should().NotBe(null);
            rectangle2.Should().NotBe(null);
            rectangle1.Should().NotBeEquivalentTo(rectangle2);
        }

        [Test]
        public void PutTwoRectangles_AndTheyAreNotIntersected()
        {
            circularCloudLayouter = new CircularTagsCloudLayouter();
            rectangleSizes.Add(new Size(10, 20));
            rectangleSizes.Add(new Size(50, 50));

            var rectangle1 = circularCloudLayouter.PutNextRectangle(rectangleSizes[0]);
            var rectangle2 = circularCloudLayouter.PutNextRectangle(rectangleSizes[1]);

            rectangle1.IntersectsWith(rectangle2).Should().Be(false);
        }

        [Test]
        public void PutThreeRectangles_AndTheyAreNotIntersected()
        {
            circularCloudLayouter = new CircularTagsCloudLayouter();
            rectangleSizes.Add(new Size(10, 20));
            rectangleSizes.Add(new Size(50, 50));
            rectangleSizes.Add(new Size(20, 7));

            var rectangle1 = circularCloudLayouter.PutNextRectangle(rectangleSizes[0]);
            var rectangle2 = circularCloudLayouter.PutNextRectangle(rectangleSizes[1]);
            var rectangle3 = circularCloudLayouter.PutNextRectangle(rectangleSizes[2]);

            rectangle1.IntersectsWith(rectangle2).Should().Be(false);
            rectangle1.IntersectsWith(rectangle3).Should().Be(false);
            rectangle2.IntersectsWith(rectangle3).Should().Be(false);
        }

        [Test]
        [TestCase(10)]
        [TestCase(50)]
        [TestCase(100)]
        [TestCase(500)]
        [TestCase(1000)]
        public void PutManyRectangles_AndTheyAreNotIntersected(int rectanglesCount)
        {
            circularCloudLayouter = new CircularTagsCloudLayouter();
            const int randomRange = 1000;
            var random = new Random(randomRange);
            for (var i = 0; i < rectanglesCount; i++)
                rectangleSizes.Add(new Size(random.Next(1, randomRange), random.Next(1, randomRange)));
            var rectangles = new Rectangle[rectanglesCount];
            for (var i = 0; i < rectanglesCount; i++)
                rectangles[i] = circularCloudLayouter.PutNextRectangle(rectangleSizes[i]);
            rectangles
                .SelectMany(r1 => rectangles.Select((r2) => r1 != r2 && r1.IntersectsWith(r2)))
                .Any(x => x)
                .Should()
                .BeFalse();
        }

        [Test]
        [TestCase(10)]
        [TestCase(50)]
        [TestCase(100)]
        [TestCase(500)]
        [TestCase(1000)]
        public void PutManyRectangles_AndTagCloudIsCircularity(int rectanglesCount)
        {
            circularCloudLayouter = new CircularTagsCloudLayouter();
            const int randomRange = 200;
            var random = new Random(randomRange);
            for (var i = 0; i < rectanglesCount; i++)
                rectangleSizes.Add(new Size(1 + random.Next(randomRange), 1 + random.Next(randomRange)));

            double maxDistanceFromCenter = 0;
            double tagCloudSquare = 0;
            for (var i = 0; i < rectanglesCount; i++)
            {
                var rectangle = circularCloudLayouter.PutNextRectangle(rectangleSizes[i]);
                var vertices = new Point[]
                {
                    new Point(rectangle.Left, rectangle.Bottom),
                    new Point(rectangle.Left, rectangle.Top),
                    new Point(rectangle.Right, rectangle.Bottom),
                    new Point(rectangle.Right, rectangle.Top),
                };
                var theMostFarVertexFromCenter = vertices
                    .Select((x) => CalcDistanceBetweenPoints(cloudCenter, x))
                    .Max();
                maxDistanceFromCenter = Math.Max(maxDistanceFromCenter,
                    theMostFarVertexFromCenter);
                tagCloudSquare += rectangleSizes[i].Width * rectangleSizes[i].Height;
            }
            var circleArea = Math.PI * maxDistanceFromCenter * maxDistanceFromCenter;
            var squaresRatio = tagCloudSquare / circleArea;
            squaresRatio.Should().BeGreaterOrEqualTo(0.3);
        }

        [Test]
        [TestCase(10)]
        [TestCase(50)]
        [TestCase(100)]
        [TestCase(500)]
        [TestCase(1000)]
        public void PutManyRectangles_AndTheyAreTight(int rectanglesCount)
        {
            circularCloudLayouter = new CircularTagsCloudLayouter();
            const int randomRange = 1000;
            var random = new Random(randomRange);
            for (var i = 0; i < rectanglesCount; i++)
                rectangleSizes.Add(new Size(random.Next(1, randomRange), random.Next(1, randomRange)));
            var rectangles = new List<Rectangle>();
            foreach (var curRectSize in rectangleSizes)
                rectangles.Add(circularCloudLayouter.PutNextRectangle(curRectSize));
            foreach (var rectangle in rectangles)
                CanRectangleBeMovedToCentre(rectangle, rectangles).Should().BeFalse();
        }


        private double CalcDistanceBetweenPoints(Point a, Point b) =>
            Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));

        private bool CanRectangleBeMovedToCentre(Rectangle rectangle,
            IEnumerable<Rectangle> rectangles)
        {
            var cX = rectangle.X < cloudCenter.X ? 1 : rectangle.X > cloudCenter.X ? -1 : 0;
            var cY = rectangle.Y < cloudCenter.Y ? 1 : rectangle.Y > cloudCenter.Y ? -1 : 0;
            rectangle.X += cX;
            if (cX != 0 &&
                rectangles.
                Where(r => !r.Equals(rectangle))
                .All(r => !r.IntersectsWith(rectangle)))
                return true;
            rectangle.X -= cX;
            rectangle.Y += cY;
            if (cY != 0 &&
                rectangles.
                Where(r => !r.Equals(rectangle))
                .All(r => !r.IntersectsWith(rectangle)))
                return true;
            rectangle.Y -= cY;
            return false;
        }
    }
}