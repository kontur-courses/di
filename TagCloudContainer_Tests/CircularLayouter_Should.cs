using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagCloudContainer;
using TagCloudContainer.Api;
using TagCloudContainer.Implementations;

namespace TagCloudContainer_Tests
{
    [TestFixture]
    public class CircularLayouter_Should
    {
        private CircularCloudLayouter layouter;
        private List<Rectangle> layout;
        private IRectanglePenProvider penProvider;
        private DrawingOptions drawingOptions;

        [SetUp]
        public void SetUp()
        {
            layouter = new CircularCloudLayouter();
            layout = new List<Rectangle>();
            drawingOptions = new DrawingOptions();
            penProvider = new OneColorPenProvider(drawingOptions);
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status.Equals(TestStatus.Failed))
            {
                var fileName = $"{TestContext.CurrentContext.Test.Name}_rects.bmp";
                var img = new LayoutVisualizer(penProvider, layout, drawingOptions).CreateImageWithRectangles();
                img.Save(fileName);
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
                layouter.PutNextRectangle(size, layout);
            }

            layout.Any(r => layout.Any(s => r != s && s.IntersectsWith(r))).Should().BeFalse();
        }

        [Test]
        public void TestPlacesFirstRectInCentre()
        {
            var size = new Size(200, 200);
            var expected = new Rectangle(-100, -100, 200, 200);
            var rect = layouter.PutNextRectangle(size, layout);

            rect.Should().BeEquivalentTo(expected);
        }
    }
}