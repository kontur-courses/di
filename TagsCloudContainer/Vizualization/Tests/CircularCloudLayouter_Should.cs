using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace TagsCloudContainer
{
    [TestFixture]
    public class CircularCloudLayouter_Should
    {
        private readonly CircularCloudVisualizer visualizer = new CircularCloudVisualizer();
        private CircularCloudLayouter layouter;
        private static string TestDirectoryPath => Path.Combine(Directory.GetCurrentDirectory(), @"\visualization\");

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Failure)
            {
                return;
            }

            var fullPath = Path.Combine(TestDirectoryPath, TestContext.CurrentContext.Test.FullName + ".png");
            var image = visualizer.VisualizeLayout(layouter);
            image.Save(fullPath, ImageFormat.Png);
            TestContext.WriteLine($"Tag cloud visualization saved to file {fullPath}");
        }

        [Test]
        public void PutInCenter_OnSingleRectangle()
        {
            layouter = new CircularCloudLayouter(new Point(500, 500));
            layouter.PutNextRectangle(new Size(200, 100)).Should().Be(new Rectangle(500, 500, 200, 100));
        }

        [TestCase(1000, 1000, 10, 30, 30, 10, TestName = "onIncreasingRectangles")]
        [TestCase(1000, 1000, 10, 200, 150, -10, TestName = "onDecreasingRectangles")]
        [TestCase(1000, 1000, 10, 30, 30, 0, TestName = "onSameSquares")]
        [TestCase(0, 0, 10, 30, 30, 0, TestName = "onZeroCenter")]
        [TestCase(1000, 1000, 10, 60, 30, 0, TestName = "onSameHorizontalRectangles")]
        [TestCase(1000, 1000, 10, 30, 60, 0, TestName = "onSameVerticalRectangles")]
        public void PutRectanglesCorrectly(int centerX, int centerY, int count, int startWidth, int startHeight,
            int step)
        {
            var center = new Point(centerX, centerY);
            layouter = new CircularCloudLayouter(center);
            var sizes = CreateSizeList(count, startWidth, startHeight, step);
            foreach (var size in sizes)
            {
                layouter.PutNextRectangle(size);
            }

            layouter.GetRectangles().Count.Should().Be(sizes.Count);
            ContainsIntersectedRectangles(layouter).Should().Be(false);
        }

        [Timeout(1000)]
        [TestCase(200, TestName = "on 200 rectangles")]
        [TestCase(500, TestName = "on 500 rectangles")]
        [TestCase(1000, TestName = "on 1000 rectangles")]
        public void HaveCorrectTime_OnManyRectangles(int count)
        {
            layouter = new CircularCloudLayouter(new Point(500, 500));
            for (var i = 0; i < count; i++)
            {
                layouter.PutNextRectangle(new Size(40, 20));
            }

            layouter.GetRectangles().Count.Should().Be(count);
        }

        private static List<Size> CreateSizeList(int count, int startWidth, int startHeight, int step)
        {
            var sizeList = new List<Size>();
            var width = startWidth;
            var height = startHeight;
            for (var i = 0; i < count; i++)
            {
                width += step;
                height += step;
                sizeList.Add(new Size(width, height));
            }

            return sizeList;
        }

        private static bool ContainsIntersectedRectangles(CircularCloudLayouter layouter)
        {
            var rectangles = layouter.GetRectangles();
            foreach (var firstRectangle in rectangles)
            {
                foreach (var secondRectangle in rectangles)
                {
                    if (firstRectangle != secondRectangle && firstRectangle.IntersectsWith(secondRectangle))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}