using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloud.Core;

namespace TagsCloud.Tests
{
    internal class CircularCloudLayouter_Tests
    {
        private const double MinDensity = 0.5;
        public const int ImageWidth = 1920;
        public const int ImageHeight = 1080;
        private readonly SizeGenerator generator = new SizeGenerator(10, 40, 10, 20);
        private Point center;
        private CircularCloudLayouter cloud;
        private IEnumerable<Rectangle> rectangles;

        [SetUp]
        public void SetUp()
        {
            center = new Point(ImageWidth / 2, ImageHeight / 2);
            cloud = new CircularCloudLayouter(center);
        }

        [TestCase(-1, 1, TestName = "WhenNotPositiveWidth")]
        [TestCase(1, -1, TestName = "WhenNotPositiveHeight")]
        public void PutNextRectangle_ThrowException(int width, int height)
        {
            Assert.Throws<ArgumentException>(() => cloud.PutNextRectangle(new Size(width, height)));
        }

        [TestCase(1, TestName = "WhenAdd1Rectangle")]
        [TestCase(10, TestName = "WhenAdd10Rectangle")]
        [TestCase(100, TestName = "WhenAdd100Rectangle")]
        public void PutNextRectangle_CorrectCountOfRectangles(int count)
        {
            rectangles = generator.GenerateSize(count).Select(cloud.PutNextRectangle);

            rectangles.Should().HaveCount(count);
        }

        [Test]
        public void PutNextRectangle_FirstRectangleOnCenter()
        {
            center = new Point(1, 2);
            cloud = new CircularCloudLayouter(center);

            var rect = cloud.PutNextRectangle(new Size(10, 10));
            var rectCenter = new Point(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);

            rectCenter.Should().Be(center);
        }

        [Timeout(1000)]
        [TestCase(1000, TestName = "WhenAdd1000RandomRectangles")]
        public void PutNextRectangle_RectanglesDoesNotIntersect(int count)
        {
            rectangles = generator.GenerateSize(count).Select(cloud.PutNextRectangle).ToList();

            foreach (var rect in rectangles)
                rect.IntersectsWith(rectangles.Where(x => x != rect)).Should().BeFalse();
        }

        [Timeout(1000)]
        [TestCase(1000, TestName = "WhenAdd1000RandomRectangles")]
        public void PutNextRectangle_PlacedRectanglesHasNearlyCircleShape(int count)
        {
            rectangles = generator.GenerateSize(count).Select(cloud.PutNextRectangle).ToList();

            var top = center.Y - rectangles.Min(rect => rect.Top);
            var left = center.X - rectangles.Min(rect => rect.Left);
            var right = rectangles.Max(rect => rect.Right) - center.X;
            var bottom = rectangles.Max(rect => rect.Bottom) - center.Y;
            var radius = Math.Max(Math.Max(top, bottom), Math.Max(left, right));
            var fault = radius / 3.0;

            foreach (var rect in rectangles)
                GetDistanceFromPointToCenter(rect.Location).Should().BeLessThan(radius + fault);
        }

        [Timeout(1000)]
        [TestCase(1000, TestName = "WhenAdd1000RandomRectangles")]
        public void PutNextRectangle_RectanglesPlacedTightly(int count)
        {
            rectangles = generator.GenerateSize(count).Select(cloud.PutNextRectangle).ToList();
            var allRectanglesArea = rectangles.Select(x => x.Width * x.Height).Sum();

            var radius = rectangles.Max(x => GetDistanceFromPointToCenter(x.Location));
            var circleArea = Math.PI * radius * radius;
            var areaRatio = allRectanglesArea / circleArea;

            areaRatio.Should().BeGreaterOrEqualTo(MinDensity);
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed)
                return;
            var path = $"../../Images/{TestContext.CurrentContext.Test.FullName}.jpg";
            Console.WriteLine($"Tag cloud visualization saved to file {path}");
            CircularCloudVisualization.CreateImage(rectangles, ImageWidth, ImageHeight).Save(path);
        }

        private double GetDistanceFromPointToCenter(Point point)
        {
            return Math.Sqrt(Math.Pow(point.X - center.X, 2) + Math.Pow(point.Y - center.Y, 2));
        }
    }
}