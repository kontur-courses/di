using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.CloudLayouter;
using TagCloud.CloudVisualizer;
using TagCloud.Extensions;
using TagCloud.Models;
using TagCloud.PointsSequence;

namespace TagCloud.Tests
{
    [TestFixture]
    public class CircularCloudLayouter_Should
    {
        private List<Rectangle> currentLayout;

        [SetUp]
        public void SetUp()
        {
            currentLayout = new List<Rectangle>();
        }

        [TearDown]
        public void TearDown()
        {
            var context = TestContext.CurrentContext;
            if (context.Result.FailCount == 0) return;

            var visualizer = new CloudVisualizer.CloudVisualizer
            {
                Settings = DrawSettings.RectanglesWithNumeration
            };

            var testName = string.Join("_",
                context.Test.FullName.Split(' '), StringSplitOptions.RemoveEmptyEntries);

            var path = Path.Combine(Directory.GetCurrentDirectory(),$"{context.Test.MethodName}_{testName}.png");
            var items = currentLayout.Select(r => new CloudItem(null, r));
            var picture = visualizer.CreatePictureWithItems(items.ToArray());
            picture.Save(path);

            TestContext.WriteLine($"Tag cloud visualization saved to file {path}");
        }

        [TestCase(100, 100, 150, 150, TestName = "For size 100x100 then center is (150,150)")]
        [TestCase(50, 50, 0, 0, TestName = "For size 50x50 then center is (0,0)")]
        [TestCase(250, 1, -15, -56, TestName = "For size 250x1 then center is (-15,-56)")]
        public void ReturnCorrectRectangle_AfterPuttingRectangleSize(int width, int height, int centerX, int centerY)
        {
            var center = new Point(centerX, centerY);
            var spiral = new SpiralBuilder()
                .WithCenterIn(center)
                .Build();
            var tagCloud = new CircularCloudLayouter(spiral);

            var result = tagCloud.PutNextRectangle(new Size(width, height));

            result.Should().BeEquivalentTo(new Rectangle(center.X - width / 2, center.Y - height / 2,
                width, height));
        }

        [TestCase(0, 0, TestName = "With empty Size")]
        [TestCase(-10, 1, TestName = "With negative width")]
        [TestCase(1, -10, TestName = "With negative height")]
        [TestCase(-100, -20, TestName = "With negative size")]
        public void ThrowArgumentException_AfterPuttingRectangle(int width, int height)
        {
            var spiral = new Spiral();
            var tagCloud = new CircularCloudLayouter(spiral);
            Action putting = () => tagCloud.PutNextRectangle(new Size(width, height));

            putting.Should().Throw<ArgumentException>();
        }

        [TestCase(0, TestName = "0 times")]
        [TestCase(1, TestName = "1 times")]
        [TestCase(259, TestName = "259 times")]
        public void ReturnCorrectCountOfRectangles_AfterPuttingRectangles(int count)
        {
            var spiral = new Spiral();
            var tagCloud = new CircularCloudLayouter(spiral);

            for (var i = 0; i < count; i++)
                tagCloud.PutNextRectangle(new Size(1, 1));

            tagCloud.Count.Should().Be(count);
        }

        [TestCase(10, 10, 10, 10, TestName = "Then rectangles are similar")]
        [TestCase(10, 10, 15, 15, TestName = "Then second is bigger")]
        [TestCase(125, 125, 50, 50, TestName = "Then first is bigger")]
        [TestCase(5000, 4000, 3555, 6000, TestName = "Then both are huge")]
        [TestCase(1, 1, 1, 1, TestName = "Then both are points")]
        public void PutTwoRectanglesInDifferentLocation(int widthOfFirst, int heightOfFirst, int widthOfSecond, int heightOfSecond)
        {
            var spiral = new Spiral();
            var tagCloud = new CircularCloudLayouter(spiral);

            currentLayout.Add(tagCloud.PutNextRectangle(new Size(widthOfFirst, heightOfFirst)));
            currentLayout.Add(tagCloud.PutNextRectangle(new Size(widthOfSecond, heightOfSecond)));

            currentLayout[0].IntersectsWith(currentLayout[1])
                .Should().BeFalse();
        }

        [TestCase(2, TestName = "Then 2 rectangles")]
        [TestCase(100, TestName = "Then 100 rectangles")]
        public void PutRectanglesInFreeSpace(int amountOfRectangles)
        {
            var spiral = new Spiral();
            var tagCloud = new CircularCloudLayouter(spiral);
            var rnd = new Random();

            for (var i = 0; i < amountOfRectangles; i++)
                currentLayout.Add(tagCloud.PutNextRectangle(new Size(rnd.Next(1, 1000), rnd.Next(1, 1000))));

            GetIntersectedRectanglesCount().Should().Be(0);
        }

        public int GetIntersectedRectanglesCount()
        {
            var count = 0;
            foreach (var curRect in currentLayout)
            {
                var rectanglesWithoutCurRect = currentLayout
                    .Where(r => r != curRect)
                    .ToList();
                count += rectanglesWithoutCurRect
                    .FindAll(rect => rect.IntersectsWith(curRect))
                    .Count;
            }

            return count;
        }

        [TestCase(1, TestName = "For 1 rectangle")]
        [TestCase(2, TestName = "For 2 rectangles")]
        [TestCase(100, TestName = "For 100 rectangles")]
        public void PlaceRectanglesInCircle_WhenRectanglesAreAlmostSimilar(int amountOfRectangles)
        {
            var spiral = new Spiral();
            var tagCloud = new CircularCloudLayouter(spiral);

            for (var i = amountOfRectangles; i > 0; i--)
                currentLayout.Add(tagCloud.PutNextRectangle(new Size(100 + i, 100 + i)));
            var size = currentLayout
                .SelectMany(rectangle => new[] { rectangle.Location, new Point(rectangle.Right, rectangle.Bottom) })
                .ToArray()
                .GetBounds();

            size.Width.Should().BeCloseTo(size.Height, 100);
        }
    }
}