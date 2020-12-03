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
    public class RectangleLayouterTests
    {
        [SetUp]
        public void SetUp()
        {
            random = new Random(3559);
            cloud = new RectangleLayouter(new Point(1000, 1000));
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed)
                return;
            var path = Directory.GetCurrentDirectory() + $"\\{TestContext.CurrentContext.Test.Name}.bmp";
            RectanglePainter.DrawRectanglesInFile(cloud.GetAllRectangles(), path);
            Console.WriteLine("Tag cloud visualization saved to file " + path);
        }

        private RectangleLayouter cloud;
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

            var height = rectangles.Max(x => x.Bottom) - cloud.Location.Y;
            var weight = rectangles.Max(x => x.Right) - cloud.Location.X;


            ((double) square / (height * weight)).Should().BeGreaterThan(0.5);
        }

        [Test]
        public void ClearLayout_RectanglesBeforeClear_AfterClearPutRectangleInLocation()
        {
            for (var i = 0; i < 10; i++)
            {
                var size = new Size(random.Next(1, 100), random.Next(1, 100));
                cloud.PutNextRectangle(size);
            }

            cloud.ClearLayout();
            cloud.PutNextRectangle(new Size(100, 100)).Location.Should().Be(cloud.Location);
        }
    }
}