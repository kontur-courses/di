using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using NUnit.Framework;
using TagsCloudVisualization;
using TagsCloudVisualizationTests.Interfaces;
using TagsCloudVisualizationTests.TestingLibrary.RectangleStyles;

namespace TagsCloudVisualizationTests.TestingLibrary
{
    public class LayouterBitmapSaver
    {
        private CircularCloudLayouter defaultLayouter;

        [SetUp]
        public void SetUp()
        {
            defaultLayouter = new CircularCloudLayouter();
        }

        public static List<Size> CreateRandomRectangles(int count)
        {
            var random = new Random();
            return Enumerable.Range(0, count).Select(
                _ =>
                {
                    var width = random.Next(40, 100);
                    var height = width / 5 + random.Next(-5, 5);
                    return new Size(width, height);
                }).ToList();
        }

        [Test]
        [Explicit]
        public void PutNextRectangle_Squares_SaveToBitmap()
        {
            var square = new Size(10, 10);
            var rectangles = Enumerable.Range(0, 1000).Select(_ => defaultLayouter.PutNextRectangle(square));
            SaveRectanglesToBitmap(rectangles, new RedPenStyle(), "squares-red");
        }

        [TestCaseSource(nameof(RandomRectanglesStyles))]
        [Explicit]
        public void CircularCloudLayouter_ColoredFillRandomRectangles_SaveToBitmap(
            IRectangleStyle style,
            string postfix)
        {
            var rectangles = CreateRandomRectangles(1000)
                .Select(defaultLayouter.PutNextRectangle);

            SaveRectanglesToBitmap(rectangles, style, postfix);
        }

        private static IEnumerable<TestCaseData> RandomRectanglesStyles()
        {
            yield return new TestCaseData(new RedPenStyle(), "random-red");
            yield return new TestCaseData(new ColoredStyle(), "random-colored");
            yield return new TestCaseData(new ColoredFillStyle(), "random-colored-fill");
        }

        private static void SaveRectanglesToBitmap(
            IEnumerable<Rectangle> rectangles,
            IRectangleStyle style,
            string filenamePostfix = "")
        {
            var visualizer = new RectangleVisualizer(rectangles) {Style = style};
            var savePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                $"CircularCloudLayouter.Rectangles-{filenamePostfix}.bmp");

            new VisualOutput(visualizer).SaveToBitmap(savePath);
            TestContext.WriteLine($"Saved to '{savePath}'");
        }
    }
}