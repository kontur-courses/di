using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using CloudLayouters;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace CircularCloudTests
{
    [TestFixture]
    public class CircularCloud_Should
    {
        [SetUp]
        public void SetUp()
        {
            random = new Random();
            cloud = new CircularCloudLayouter(new Point(1000, 1000));
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed)
                return;
            RectanglePainter.DrawRectanglesInFile(cloud.GetAllRectangles());
            Console.WriteLine("Tag cloud visualization saved to file" + Directory.GetCurrentDirectory() +
                              "\\visualisation.bmp");
        }

        private CircularCloudLayouter cloud;
        private Random random;


        [Test]
        public void PutNextRectangle_1000RandomSizes_ReturnedRectanglesShouldNotIntersect()
        {
            var rectangles = new List<Rectangle>();
            for (var i = 0; i < 100; i++)
                rectangles.Add(cloud.PutNextRectangle(new Size(random.Next(1, 100), random.Next(1, 100))));
            rectangles.Any(rectangle => rectangles.Any(otherRectangle =>
                    rectangle != otherRectangle && rectangle.IntersectsWith(otherRectangle))).Should()
                .BeFalse("some rectangles intersect");
        }

        [Timeout(20000)]
        [Test]
        public void PutNextRectangle_1000RandomSizes_ShouldFinishFasterThan20Seconds()
        {
            for (var i = 0; i < 1000; i++)
                cloud.PutNextRectangle(new Size(random.Next(1, 10), random.Next(1, 10)));
        }

        [Test]
        public void PutNextRectangle_100RandomSizes_ReturnRectangleWithSameSize()
        {
            for (var i = 0; i < 100; i++)
            {
                var size = new Size(random.Next(1, 100), random.Next(1, 100));
                var rectangle = cloud.PutNextRectangle(size);
                rectangle.Size.Should().Be(size);
            }
        }

        [Test]
        public void PutNextRectangle_100RandomSizes_ReturnedRectanglesShouldBeCloseToEachOther()
        {
            var rectangles = new List<Rectangle>();
            var square = 0;
            for (var i = 0; i < 100; i++)
            {
                var rectangle = cloud.PutNextRectangle(new Size(random.Next(1, 100), random.Next(1, 100)));
                square += rectangle.Size.Height * rectangle.Size.Width;
                rectangles.Add(rectangle);
            }


            var radius = GetSmallestRadiusForRectangles(rectangles);
            (square / (Math.PI * radius * radius)).Should().BeGreaterThan(0.5);
        }

        private double GetSmallestRadiusForRectangles(IEnumerable<Rectangle> rectangles)
        {
            return rectangles.Max(rectangle =>
            {
                var pointsOfCorners = new[]
                {
                    new Size(rectangle.Size.Width, 0),
                    new Size(rectangle.Size.Width, rectangle.Size.Height),
                    new Size(0, rectangle.Size.Height),
                    new Size(0, 0)
                };
                return pointsOfCorners.Max(size =>
                {
                    var point = rectangle.Location + size - new Size(cloud.Center);
                    return Math.Sqrt(point.X * point.X + point.Y * point.Y);
                });
            });
        }

        [Test]
        public void PutNextRectangle_OneRectangle_FirstRectangleShouldBePlacedInCenter()
        {
            cloud.PutNextRectangle(new Size(100, 100)).Location.Should().Be(new Point(950, 950));
        }
    }
}