using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace TagsCloudVisualization
{
    [TestFixture]
    public class CircularCloudLayouter_Should
    {
        private CircularCloudLayouter cloudLayouter;

        [SetUp]
        public void CreateLayouter() => 
            cloudLayouter = new CircularCloudLayouter(new Point(500,500));

        [TearDown]
        public void MakeImageOfFailedTests()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed) return;

            TagCloudVisualizatior.DrawAndSaveAtDocumentFolder(cloudLayouter.GetRectangles(),
                                    TestContext.CurrentContext.Test.Name, 1000, 1000);

            Console.WriteLine($"Tag cloud visualization saved to file <{Directory.GetCurrentDirectory()}>");
        }

        [TestCase(0, 12, TestName = "Width = 0")]
        [TestCase(13, 0, TestName = "Height = 0")]
        [TestCase(0, 0, TestName = "Width = 0, Height = 0")]
        [TestCase(-123, 0, TestName = "Width < 0, Height = 0")]
        [TestCase(0, -444, TestName = "Width = 0, Height < 0")]
        [TestCase(-31, -1, TestName = "Width < 0, Height < 0")]
        [TestCase(-31, 2, TestName = "Width < 0")]
        [TestCase(3, -1, TestName = "Height < 0")]
        public void PutNextRectangle_SizeElementsLessOrEqualZero_ThrowArgumentException(int width, int height)
        {
            Calling.ThisLambda(() => cloudLayouter.PutNextRectangle(new Size(width, height)))
                .Should().Throw<ArgumentException>();
        }

        [TestCase(1, 1, TestName = "Minimal rectangle")]
        [TestCase(10, 31, TestName = "Small rectangle")]
        [TestCase(4096, 4096, TestName = "Big rectangle")]
        public void PutNextRectangle_SizeElementsMoreThanZero_NotThrowArgumentException(int width, int height)
        {
            Calling.ThisLambda(() => cloudLayouter.PutNextRectangle(new Size(width, height)))
                .Should().NotThrow<ArgumentException>();
        }

        [TestCase(0, 0, TestName = "Zero coords")]
        [TestCase(-12, -3, TestName = "Negative coords")]
        [TestCase(21, 7, TestName = "Positive coords")]
        [TestCase(21, -5, TestName = "Positive and negative coords")]
        public void PutNextRectangle_PutOneRectangle_NeedBeOnCenterPoint(int centerX, int centerY)
        {
            var newCloutLayouter = new CircularCloudLayouter(new Point(centerX, centerY));

            var rectangle = newCloutLayouter.PutNextRectangle(new Size(213, 313));
            rectangle.Location.Should().Be(newCloutLayouter.Center);
        }

        [TestCase(1, 1, 1, 1, TestName ="Two minimal rectangles")]
        [TestCase(500, 350, 1, 1, TestName ="Minimal and big rectangles")]
        [TestCase(85, 35, 62, 74, TestName ="Two rectangles")]
        [TestCase(432, 305, 232, 504, TestName ="Two big rectangles")]
        public void PutNextRectangle_PutTwoRectangles_TwoRectanglesMustNotIntersect(int firstWidth, int firstHeight, int secondWidth, int secondHeight)
        {
            var rect1 = cloudLayouter.PutNextRectangle(new Size(firstWidth, firstHeight));
            var rect2 = cloudLayouter.PutNextRectangle(new Size(secondWidth, secondHeight));

            rect1.IntersectsWith(rect2).Should().BeFalse();
        }

        [TestCase(50, 200, TestName = "Fifty rectangles")]
        [TestCase(50, 2000, TestName = "Fifty rectangles, big size")]
        [TestCase(100, 200, TestName = "One hundred rectangles")]
        [TestCase(1000, 200, TestName = "One thousand rectangles")]
        [TestCase(3000, 2000, TestName = "Three thousand rectangles, big size")]
        public void PutNextRectangle_PutRandomRectangles_RectanglesMustNotIntersect(int rectanglesCount, int maxSizeParam)
        {
            var randomizer = new Random();
            var rectangles = new List<Rectangle>();
            for (var i = 0; i < rectanglesCount; i++)
            {
                var size = new Size(randomizer.Next(1, maxSizeParam), 
                                    randomizer.Next(1, maxSizeParam));
                rectangles.Add(cloudLayouter.PutNextRectangle(size));
            }

            for (var i = 0; i < rectangles.Count; i++)
                for (var j = i + 1; j < rectangles.Count; j++)
                    rectangles[i].IntersectsWith(rectangles[j]).Should().BeFalse();
        }


        [TestCase(50, 200, TestName = "Fifty rectangles")]
        [TestCase(50, 2000, TestName = "Fifty rectangles, big size")]
        [TestCase(100, 200, TestName = "One hundred rectangles")]
        [TestCase(1000, 200, TestName = "One thousand rectangles")]
        public void PutNextRectangle_PutRandomRectangles_TagCloudDestinyNeedBeModerate(int rectanglesCount, int maxSizeParam)
        {
            var randomizer = new Random();

            for (var i = 0; i < rectanglesCount; i++)
            {
                var size = new Size(randomizer.Next(1, maxSizeParam), 
                                    randomizer.Next(1, maxSizeParam));
                cloudLayouter.PutNextRectangle(size);
            }

            var cloudRadius = cloudLayouter.GetRectangles()
                .Select(rec => rec.GetDistanceToPoint(cloudLayouter.Center))
                .OrderBy(distance => distance)
                .First();

            var occupiedArea = cloudLayouter.GetRectangles()
                .Select(a => a.Width * a.Height)
                .Sum();

            var cloudArea = Math.PI * cloudRadius * cloudRadius;
            cloudArea /= occupiedArea;
            cloudArea.Should().BeLessThan(2);
        }
    }
}