using System;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagCloudContainer;

namespace TagCloudContainer_Tests
{
    [TestFixture]
    public class CircularLayouter_Should
    {
        private CircularCloudLayouter layouter;

        [SetUp]
        public void SetUp()
        {
            layouter = new CircularCloudLayouter();
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status.Equals(TestStatus.Failed))
            {
                string fileName = $"{TestContext.CurrentContext.Test.Name}_rects.bmp";
                var bmp = LayouterVisualizer.CreateSizedBitmapForLayouter(layouter);
                var brush = new SolidBrush(Color.White);
                var pen = new Pen(Color.Blue);
                LayouterVisualizer.SaveBitmapWithRectanglesFromLayout(layouter, fileName, brush, pen);
                Console.Error.WriteLine($"Tag cloud visualization saved to file {fileName}");
            }
        }

        [TestCase(100, 8, 40, 4, 20, TestName = "RectanglesDoNotIntersectEachOther_OnHundredShortRectangles")]
        [TestCase(100, 30, 200, 12, 32, TestName = "RectanglesDoNotIntersectEachOther_OnHundredLongRectangles")]
        public void TestRectanglesDoNotIntersectEachOther(int rectCount, int minWidth, int maxWidth, int minHeight,
            int maxHeight)
        {
            var random = new Random();
            for (int i = 0; i < rectCount; i++)
            {
                var size = new Size(random.Next(minWidth, maxWidth), random.Next(minHeight, maxHeight));
                layouter.PutNextRectangle(size);
            }

            layouter.Layout.Any(r => layouter.Layout.Any(s => r != s && s.IntersectsWith(r)))
                .Should().BeFalse();
        }

        [Test]
        public void TestPlacesFirstRectInCentre()
        {
            var size = new Size(200, 200);
            var expected = new Rectangle(-100, -100, 200, 200);
            var rect = layouter.PutNextRectangle(size);

            rect.Should().BeEquivalentTo(expected);
        }
    }
}