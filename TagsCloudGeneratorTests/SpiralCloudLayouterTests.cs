using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudGenerator.Core.Drawers;
using TagsCloudGenerator.Core.Layouters;
using TagsCloudGenerator.Core.Spirals;
using TagsCloudGenerator.Infrastructure;

namespace TagsCloudGeneratorTests
{
    [TestFixture]
    public class SpiralCloudLayouterTests
    {
        private SpiralRectangleCloudLayouter layouter;
        private List<Rectangle> rectangles;
        private Random random;
        private Point center;

        [SetUp]
        public void SetUp()
        {
            layouter = new SpiralRectangleCloudLayouter(
                new ArchimedeanSpiral(1, 0.05f));
            rectangles = new List<Rectangle>();
            random = new Random(25);
            center = new Point(0, 0);
        }

        [TestCase(5, TestName = "WhenPut5Rectangles")]
        [TestCase(25, TestName = "WhenPut25Rectangles")]
        [TestCase(50, TestName = "WhenPut50Rectangles")]
        public void PutNextRectangle_ReturnsDisjointRectangles(int count)
        {
            PutRandomRectangles(count);
            IsDisjointRectangles(rectangles).Should().BeTrue();
        }

        [TestCase(5, TestName = "WhenPut5Rectangles")]
        [TestCase(55, TestName = "WhenPut55Rectangles")]
        [TestCase(100, TestName = "WhenPut100Rectangles")]
        public void PutNextRectangle_ShouldIncreaseRectanglesCount(int count)
        {
            PutRandomRectangles(count);
            rectangles.Count.Should().Be(count);
        }

        [Test]
        public void PutNextRectangle_ShouldPutFirstRectangleInCenter()
        {
            var size = new Size(50, 70);
            var rectangle = layouter.PutNextRectangle(size);
            var expectedLocation = new Point(center.X - size.Width / 2, center.Y - size.Height / 2);
            rectangle.X.Should().BeInRange(
                expectedLocation.X - 1,
                expectedLocation.X + 1);
            rectangle.Y.Should().BeInRange(
                expectedLocation.Y - 1,
                expectedLocation.Y + 1);
        }

        [TestCase(10, TestName = "WhenPut10Rectangles")]
        [TestCase(30, TestName = "WhenPut30Rectangles")]
        [TestCase(50, TestName = "WhenPut50Rectangles")]
        public void PutNextRectangle_ShouldPutRectanglesTightly(int rectanglesCount)
        {
            var size = new Size(10, 10);
            for (var i = 0; i < rectanglesCount - 1; i++)
                layouter.PutNextRectangle(size);

            var rect = layouter.PutNextRectangle(size);
            Math.Abs(rect.X - center.X).Should()
                .BeLessThan(rectanglesCount * size.Width / (rectanglesCount / 4));
            Math.Abs(rect.Y - center.Y).Should()
                .BeLessThan(rectanglesCount * size.Height / (rectanglesCount / 4));
        }

        [TestCase(10, TestName = "WhenPut10Rectangles")]
        [TestCase(30, TestName = "WhenPut30Rectangles")]
        [TestCase(50, TestName = "WhenPut50Rectangles")]
        public void PutNextRectangle_ShouldArrangeRectanglesAsCircle(int count)
        {
            var rectanglesArea = 0;
            foreach (var size in GenerateRandomSizes(count))
            {
                var rectangle = layouter.PutNextRectangle(size);
                rectangles.Add(rectangle);
                rectanglesArea += rectangle.Width * rectangle.Height;
            }

            var increasedRadius = Math.Sqrt(rectanglesArea / Math.PI) * 1.25;
            foreach (var rectangle in rectangles)
                rectangle.DistanceToPoint(center).Should().BeLessThan(increasedRadius);
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Failure)
                return;
            var failedTestsPath = TestContext.CurrentContext.TestDirectory + @"\FailedTests";
            if (!Directory.Exists(failedTestsPath))
                Directory.CreateDirectory(failedTestsPath);
            var cloudDrawer = new RectangleCloudDrawer(Color.Teal, Brushes.Peru, Brushes.Black);
            var filename = failedTestsPath + $"\\{TestContext.CurrentContext.Test.FullName}.png";
            var bitmap = cloudDrawer.DrawRectangles(rectangles);
            bitmap.Save(filename);
            TestContext.WriteLine($"Tag cloud visualisation saved to file: '{filename}'");
        }

        private IEnumerable<Size> GenerateRandomSizes(int count)
        {
            for (var i = 0; i < count; i++)
                yield return new Size(random.Next(1, 1000), random.Next(1, 1000));
        }

        private void PutRandomRectangles(int count)
        {
            rectangles = rectangles
                .Concat(GenerateRandomSizes(count).Select(size => layouter.PutNextRectangle(size)))
                .ToList();
        }

        private bool IsDisjointRectangles(List<Rectangle> rects)
        {
            for (var i = 0; i < rects.Count; i++)
            {
                for (var j = i + 1; j < rects.Count; j++)
                {
                    if (rects[i].IntersectsWith(rects[j]))
                        return false;
                }
            }

            return true;
        }
    }
}