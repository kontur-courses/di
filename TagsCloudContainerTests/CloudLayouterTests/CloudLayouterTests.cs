using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.CloudLayouters;
using TagsCloudContainer.CloudLayouters.PointGenerators;
using TagsCloudContainer.Settings;

namespace TagsCloudContainerTests.CloudLayouterTests
{
    [TestFixture]
    public class CloudLayouterTest
    {
        [SetUp]
        public void SetUp()
        {
            var imageSettings = new ImageSettings(1000, 1000, "", "classic");

            pointsGenerator = new ArchimedesSpiralPointGenerator(imageSettings);
            circularCloudLayouter = new CircularCloudLayouter(pointsGenerator);
        }

        private readonly Random random = new Random();
        private CircularCloudLayouter circularCloudLayouter;
        private readonly Point centerPoint = new Point(500, 500);
        private List<Rectangle> rectangles = new List<Rectangle>();
        private IEnumerable<Point> pointsGenerator;

        [TestCase(-3, -4, TestName = "when size is negative")]
        [TestCase(-3, 4, TestName = "when size width is negative, height is positive")]
        [TestCase(3, -4, TestName = "when size width is positive, height is negative")]
        public void PutNextRectangle_ShouldThrowArgumentException(int width, int height)
        {
            var size = new Size(width, height);

            Action act = () => circularCloudLayouter.PutNextRectangle(size);

            act.ShouldThrow<ArgumentException>();
        }


        [TestCase(3, 4, TestName = "when size is positive")]
        [TestCase(0, 0, TestName = "when size is zero")]
        [TestCase(3, 0, TestName = "when size width is positive, height is zero")]
        [TestCase(0, 4, TestName = "when size width is zero, height is positive")]
        public void PutNextRectangle_DontShouldThrowArgumentException(int width, int height)
        {
            var size = new Size(width, height);

            Action act = () => circularCloudLayouter.PutNextRectangle(size);

            act.ShouldNotThrow<ArgumentException>();
        }

        [TestCase(2)]
        [TestCase(10)]
        [TestCase(50)]
        public void Cloud_ShouldNoHaveIntersectingRectangles(int countRectangles)
        {
            rectangles = GenerateRectangles(countRectangles);

            HaveIntersect(rectangles).Should().BeFalse();
        }


        private Size GetRandomSize()
        {
            var h = random.Next(30, 100);
            var w = random.Next(50, 200);
            return new Size(w, h);
        }

        private List<Rectangle> GenerateRectangles(int countRectangles)
        {
            var result = new List<Rectangle>();
            for (var i = 0; i < countRectangles; i++)
            {
                var size = GetRandomSize();
                var rectangle = circularCloudLayouter.PutNextRectangle(size);
                result.Add(rectangle);
            }

            return result;
        }

        protected bool HaveIntersect(List<Rectangle> cloudRectangles)
        {
            foreach (var rectangle1 in cloudRectangles)
            foreach (var rectangle2 in cloudRectangles)
                if (rectangle2 != rectangle1 && rectangle1.IntersectsWith(rectangle2))
                    return true;
            return false;
        }


        private IEnumerable<Point> CountPointsOutOfCircle(double circleRadius, List<Rectangle> listRectangles,
            Point circleCenterPoint)
        {
            return listRectangles.Select(ToBoundaryPoints).SelectMany(point => point).Distinct()
                .Where(point => PointOutRadius(circleRadius, circleCenterPoint, point));
        }

        private bool PointOutRadius(double circleRadius, Point circleCenterPoint, Point point)
        {
            return Math.Pow(circleCenterPoint.X - point.X, 2) + Math.Pow(circleCenterPoint.Y - point.Y, 2) >
                   circleRadius * circleRadius;
        }

        private Point[] ToBoundaryPoints(Rectangle rectangle)
        {
            var x = rectangle.X;
            var y = rectangle.Y;
            var h = rectangle.Height;
            var w = rectangle.Width;
            return new[]
            {
                new Point(x, y),
                new Point(x, y + h),
                new Point(x + w, y),
                new Point(x + w, y + h)
            };
        }

        [Test]
        public void Cloud_ShouldBeLikeCircle()
        {
            rectangles = GenerateRectangles(150);
            var rectanglesArea = rectangles
                .Select(rect => rect.Width * rect.Height)
                .Sum();
            var circleRadius = 1.5 * Math.Sqrt(rectanglesArea / Math.PI);

            CountPointsOutOfCircle(circleRadius, rectangles, centerPoint)
                .Should()
                .HaveCount(0);
        }


        [Test]
        public void PutRectangle_ShouldReturnRectangleWithTransferredSize()
        {
            var size = GetRandomSize();

            var rectangle = circularCloudLayouter.PutNextRectangle(size);

            rectangle.Size.ShouldBeEquivalentTo(size);
        }
    }
}