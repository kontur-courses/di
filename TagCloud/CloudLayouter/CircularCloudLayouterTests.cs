using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.FigurePaths;

namespace TagCloud.CloudLayouter
{
    [TestFixture]
    class CircularCloudLayouterTests
    {
        // Избавился от Should и в названии класса и в названиях методов)
        private CircularCloudLayouter cloudLayouter;

        [TestCase(-2, 5, TestName = "Negative width")]
        [TestCase(2, -5, TestName = "Negative height")]
        [TestCase(0, 5, TestName = "Zero width")]
        [TestCase(2, 0, TestName = "Zero height")]
        public void PutNextRectangle_ThrowArgumentException_WhenIncorrectDirectionsOfRectangle(int width, int height)
        {
            cloudLayouter = new CircularCloudLayouter(new Spiral(10, 10), new Point(0, 0));
            Action action = () => cloudLayouter.PutNextRectangle(new Size(width, height));

            action.Should().Throw<ArgumentException>();
        }

        [TestCase(0, 0, TestName = "Zero center")]
        [TestCase(10, 20, TestName = "Non-zero center")]
        public void AddingOneRectangle_PlaceInCenter(int cloudCenterX, int cloudCenterY)
        {
            var cloudCenter = new Point(cloudCenterX, cloudCenterY);
            var rectangleSize = new Size(20, 10);
            var expectedRectangle = new Rectangle(cloudCenter, rectangleSize);

            cloudLayouter = new CircularCloudLayouter(new Spiral(10, 10), cloudCenter);
            var actualRectangle = cloudLayouter.PutNextRectangle(rectangleSize);

            actualRectangle.Should().Be(expectedRectangle);
        }

        [TestCase(2, 2, 5, TestName = "Small rectangles")]
        [TestCase(100, 100, 10, TestName = "CommonRectangles")]
        [TestCase(100, 100, 100, TestName = "Many Rectangles")]
        public void PutNextRectangle_HaveNoIntersections(int maxWidth, int maxHeight, int rectanglesCount)
        {
            cloudLayouter = new CircularCloudLayouter(new Spiral(10, 10), new Point(0, 0));
            var rectangles = new List<Rectangle>();
            var random = new Random();
            for (var i = 0; i < rectanglesCount; i++)
            {
                var rectangle = cloudLayouter.PutNextRectangle(new Size(random.Next(1, maxWidth), random.Next(1, maxHeight)));
                rectangles.Any(rect => rect.IntersectsWith(rectangle)).Should().BeFalse();
                rectangles.Add(rectangle);
            }
        }

        [TestCase(2, 2, 10, TestName = "Small rectangles")]
        [TestCase(100, 100, 10, TestName = "CommonRectangles")]
        [TestCase(100, 100, 100, TestName = "Many Rectangles")]
        public void PutNextRectangle_PlaceTightly(int maxWidth, int maxHeight, int rectanglesCount)
        {
            cloudLayouter = new CircularCloudLayouter(new Spiral(10, 10), new Point(0, 0));
            var rectangles = new List<Rectangle>();
            var random = new Random();
            for (var i = 0; i < rectanglesCount; i++)
            {
                var rectangle = cloudLayouter.PutNextRectangle(new Size(random.Next(1, maxWidth), random.Next(1, maxHeight)));
                var deltaX = Math.Sign(rectangle.X);
                var deltaY = Math.Sign(rectangle.Y);

                var rectangleOffsetByX = new Rectangle(rectangle.Location - new Size(deltaX, 0), rectangle.Size);
                var rectangleOffsetByY = new Rectangle(rectangle.Location - new Size(0, deltaY), rectangle.Size);

                if (rectangles.Count > 0)
                    rectangles.Any(rect => rect.IntersectsWith(rectangleOffsetByX) ||
                                           rect.IntersectsWith(rectangleOffsetByY)).Should().BeTrue();

                rectangles.Add(rectangle);
            }
        }

        [TestCase(0, 0, 100, 100, 20, TestName = "Zero center")]
        [TestCase(50, 50, 100, 100, 20, TestName = "Positive center")]
        [TestCase(-50, -50, 100, 100, 20, TestName = "Negative center")]
        public void CloudRectangleProperty_CoverAllRectangles(int centerX, int centerY, int maxWidth,
            int maxHeight, int rectanglesCount)
        {
            var cloudLayouter = new CircularCloudLayouter(new Spiral(10, 10), new Point(centerX, centerY));
            var random = new Random();
            for (var i = 0; i < rectanglesCount; i++)
            {
                var rectangle = cloudLayouter.PutNextRectangle(new Size(random.Next(1, maxWidth), random.Next(1, maxHeight)));

                cloudLayouter.Rectangles.All(rect => cloudLayouter.CloudRectangle.Contains(rect)).Should()
                    .BeTrue();
            }
        }

        [TearDown]
        public void TearDown()
        {
            //if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            //{
            //    var visualizer = new CloudVisualizer(cloudLayouter);
            //    var path = Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tests")).FullName;
            //    path = Path.Combine(path, $"ID-{TestContext.CurrentContext.Test.ID}.png");
            //    visualizer.GetAndSaveImage(path);
            //    Console.WriteLine($"Tag cloud visualization saved to file {path}");
            //}
        }
    }
}
