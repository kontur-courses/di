using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudContainer.CloudLayouter;
using TagsCloudContainer.ImageSaver;
using TagsCloudContainer.Settings;

namespace TagsCloudContainerTests.CloudLayouterTests
{
    public class CircularCloudLayouter_Tests
    {
        private List<Rectangle> rectangles;
        private CircularCloudLayouter layouter;
        private Point center;
        private SizeGenerator generator;

        [SetUp]
        public void Setup()
        {
            generator = new SizeGenerator(10, 25, 10, 25);
            center = Point.Empty;
            rectangles = new List<Rectangle>();
            layouter = new CircularCloudLayouter(new ArchimedeanSpiral(new AppSettings()
                { AngleStep = Math.PI / 45, Density = 0.5 }));
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed)
                return;

            var drawer = new RectanglesDrawer();
            using var image = drawer.DrawRectangles(rectangles, new Size(800, 800));

            var fileName = TestContext.CurrentContext.Test.FullName + ".png";
            var saver = new ImageSaver();
            saver.Save(image, fileName);

            Console.WriteLine($"Tag cloud visualization saved to file {fileName}");
        }


        [TestCase(10, 0, TestName = "HeightIsZero")]
        [TestCase(0, 10, TestName = "WidthIsZero")]
        [TestCase(-10, 10, TestName = "WidthIsNegative")]
        [TestCase(10, -10, TestName = "HeightIsNegative")]
        public void PutNextRectangle_ThrowsException_WhenSizeIsInvalid(int width, int height)
        {
            var size = new Size(width, height);
            Assert.Throws<ArgumentException>(() => layouter.PutNextRectangle(size));
        }

        [Test]
        public void PutNextRectangle_LocationOfFirstRectangleIsCenter()
        {
            rectangles.Add(layouter.PutNextRectangle(new Size(10, 10)));

            rectangles.Should().ContainSingle(rect => rect.Location == center);
        }

        [TestCase(50, TestName = "WhenPut50Rectangles")]
        [TestCase(100, TestName = "WhenPut100Rectangles")]
        public void PutNextRectangle_RectanglesDoNotIntersect(int count)
        {
            foreach (var size in generator.GenerateSizes(count))
            {
                rectangles.Add(layouter.PutNextRectangle(size));
            }

            rectangles.Any(rect => rectangles.Where(r => r != rect).Any(rect.IntersectsWith)).Should().BeFalse();
        }

        [TestCase(50, TestName = "WhenPut50Rectangles")]
        [TestCase(100, TestName = "WhenPut100Rectangles")]
        [TestCase(250, TestName = "WhenPut250Rectangles")]
        public void PutNextRectangle_LayoutShouldBeCloseToCircleShape(int count)
        {
            rectangles = generator.GenerateSizes(count).Select(size => layouter.PutNextRectangle(size)).ToList();

            var top = rectangles.Max(rect => rect.Y);
            var right = rectangles.Max(rect => rect.X + rect.Width);
            var bottom = rectangles.Min(rect => rect.Y - rect.Height);
            var left = rectangles.Min(rect => rect.X);

            var radius = Math.Max(Math.Max(top, Math.Abs(bottom)), Math.Max(Math.Abs(left), right));

            foreach (var rectangle in rectangles)
            {
                CalculateDistanceBetweenPoints(center, rectangle.Location).Should().BeLessThan(radius);
            }
        }

        private static double CalculateDistanceBetweenPoints(Point firstPoint, Point secondPoint)
        {
            return Math.Sqrt(Math.Pow(secondPoint.X - firstPoint.X, 2) + Math.Pow(secondPoint.Y - firstPoint.Y, 2));
        }
    }
}