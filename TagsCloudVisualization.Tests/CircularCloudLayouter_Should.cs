using System;
using System.Drawing;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace TagsCloudVisualization.Tests
{
    public class CircularCloudLayouter_Should
    {
        private static readonly Point Center = Point.Empty;

        private CircularCloudLayouter circularCloudLayouter;

        [SetUp]
        public void SetUp()
        {
            circularCloudLayouter = new CircularCloudLayouter(Center);
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Failure) return;

            var path = FileUtils.FindFreeFileName(FileUtils.PngExtension);
            new GraphicsWriter(new TagsCloudDrawer(circularCloudLayouter.Rectangles.ToArray(), Center)).WriteToFile(path);
                TestContext.Out.WriteLine("Result layout has been saved to " + Path.GetFullPath(path));
        }

        [Test]
        public void HasOneRectangleInCenter_WhenPutsOne()
        {
            var rectangle = circularCloudLayouter.PutNextRectangle(new Size(2, 2));

            rectangle.Should().BeEquivalentTo(new Rectangle(-1, -1, 2, 2));
        }

        [TestCase(2)]
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(10)]
        [TestCase(100)]
        public void RectanglesDoNotIntersect_WhenPutsSeveral(int rectangleAmount)
        {
            var rectangles = new Rectangle[rectangleAmount];
            var random = new Random();
            for (var i = 0; i < rectangleAmount; i++)
            {
                var randomSize = new Size(random.Next(1, 100), random.Next(1, 100));
                rectangles[i] = circularCloudLayouter.PutNextRectangle(randomSize);
            }

            for (var i = 0; i < rectangleAmount; i++)
            for (var j = i + 1; j < rectangleAmount; j++)
                rectangles[i].IntersectsWith(rectangles[j]).Should().BeFalse();
        }

        [Test]
        public void LayoutLooks_LikeCircle()
        {
            const int rectangleSideSize = 10;
            const int rectanglesAmount = 200;

            for (var i = 0; i < rectanglesAmount; i++)
            {
                circularCloudLayouter.PutNextRectangle(new Size(rectangleSideSize, rectangleSideSize));
            }

            var rectangles = circularCloudLayouter.Rectangles;
            var circumscribedCircleRadius = rectangles
                .SelectMany(rectangle => rectangle.Corners())
                .Max(point => Math.Sqrt(Math.Pow(point.X, 2) + Math.Pow(point.Y, 2)));
            var circleArea = Math.PI * Math.Pow(circumscribedCircleRadius, 2);

            (rectanglesAmount * rectangleSideSize * rectangleSideSize / circleArea).Should().BeGreaterThan(0.8);
        }
    }
}
