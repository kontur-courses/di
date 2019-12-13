using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudVisualization.Layouters.CloudLayouters;
using TagsCloudVisualization.Utils;

namespace TagsCloudVisualization.Tests.Layouters
{
    [TestFixture]
    public class CircularCloudLayouterTests
    {
        [SetUp]
        public void SetUp()
        {
            generatedRectangles = new List<Rectangle>();
        }

        [TearDown]
        public void DrawPictureToDebug_OnFail()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var path = Path.Combine(
                    Directory.CreateDirectory($"{AppDomain.CurrentDomain.BaseDirectory}\\TestFailurePictures").FullName,
                    Guid.NewGuid() + ".png");
                //new Visualizer().DrawRectangles(generatedRectangles).Save(path);
                Console.WriteLine($"Tag cloud visualization saved to file {path}");
            }
        }

        private CircularCloudLayouter layouter;
        private List<Rectangle> generatedRectangles;

        [TestCase(0, 0, 5, TestName = "OnTwoRectangles")]
        [TestCase(0, 0, 5, TestName = "CenterIsZero")]
        [TestCase(5, 7, 5, TestName = "CenterIsNotZero")]
        [TestCase(0, 0, 50, TestName = "OnBigAmountOfRectangles")]
        public void PutNextRectangle_NotCausesIntersections(int centerX, int centerY, int amountOfRectangles)
        {
            var random = new Random();
            layouter = new CircularCloudLayouter(new Point(centerX, centerY));
            var rectangles = new List<Rectangle>();
            for (var i = 0; i < amountOfRectangles; i++)
            {
                var rectangle = PutNextRectangle(new Size(random.Next(1, 100), random.Next(1, 100)));
                rectangles.Any(x => x.IntersectsWith(rectangle)).Should()
                    .BeFalse("Rectangles shouldn't intersect with each other");
                rectangles.Add(rectangle);
            }
        }

        [TestCase(0, 0, 5, 5, TestName = "WhenCenterIsZero")]
        [TestCase(3, -5, 5, 5, TestName = "WhenCenterIsNotZero")]
        [TestCase(3, -5, 17, 21, TestName = "OnDifferentSizes")]
        public void PutNextRectangle_PutsFirstRectangleInCenter(int centerX, int centerY, int width, int height)
        {
            var center = new Point(centerX, centerY);
            layouter = new CircularCloudLayouter(center);
            var rect = PutNextRectangle(new Size(width, height));
            rect.Location.Should().Be(center);
        }

        [TestCase(5, 5, 5, TestName = "OnSmallSizeAnd5Rectangles")]
        [TestCase(35, 35, 5, TestName = "OnBigSizeAnd5Rectangles")]
        [TestCase(5, 5, 35, TestName = "OnSmallSizeAnd35Rectangles")]
        [TestCase(35, 35, 35, TestName = "OnBigSizeAnd35Rectangles")]
        [TestCase(5, 5, 105, TestName = "OnSmallSizeAnd105Rectangles")]
        [TestCase(35, 35, 105, TestName = "OnBigSizeAnd105Rectangles")]
        public void PutNextRectangle_FillsCircularShape(int sizeX, int sizeY, int amountOfRectangles)
        {
            var center = new Point(0, 0);
            layouter = new CircularCloudLayouter(center);
            var size = new Size(sizeX, sizeY);
            for (var i = 0; i < amountOfRectangles; i++)
                PutNextRectangle(size);
            var rectangleArea = generatedRectangles.Sum(x => x.Height * x.Width);
            var outerCircleRadius =
                generatedRectangles.Max(x => x.Location.GetDistanceTo(center)) +
                Math.Sqrt(size.Width * size.Width + size.Height * size.Height);
            var circleArea = Math.PI * outerCircleRadius * outerCircleRadius;
            rectangleArea.Should().BeGreaterOrEqualTo((int) (circleArea / 6));
        }

        [TestCase(5, 5, 5, TestName = "OnSmallSizeAnd5Rectangles")]
        [TestCase(35, 35, 5, TestName = "OnBigSizeAnd5Rectangles")]
        [TestCase(5, 5, 35, TestName = "OnSmallSizeAnd35Rectangles")]
        [TestCase(35, 35, 35, TestName = "OnBigSizeAnd35Rectangles")]
        [TestCase(5, 5, 105, TestName = "OnSmallSizeAnd105Rectangles")]
        [TestCase(35, 35, 105, TestName = "OnBigSizeAnd105Rectangles")]
        public void PutNextRectangle_PutsRectanglesInCircularShape(int sizeX, int sizeY, int amountOfRectangles)
        {
            var center = new Point(0, 0);
            layouter = new CircularCloudLayouter(center);
            var size = new Size(sizeX, sizeY);
            for (var i = 0; i < amountOfRectangles; i++)
                PutNextRectangle(size);
            var rectangleArea = generatedRectangles.Sum(x => x.Height * x.Width);
            var increasedArea = rectangleArea * 2;
            var radiusOfEquivalentCircle = Math.Sqrt(increasedArea / Math.PI);
            foreach (var rect in generatedRectangles)
                rect.Location.GetDistanceTo(center).Should().BeLessOrEqualTo(radiusOfEquivalentCircle);
        }

        [TestCase(5, 5, 5, TestName = "OnSmallSizeAnd5Rectangles")]
        [TestCase(35, 35, 5, TestName = "OnBigSizeAnd5Rectangles")]
        [TestCase(5, 5, 35, TestName = "OnSmallSizeAnd35Rectangles")]
        [TestCase(35, 35, 35, TestName = "OnBigSizeAnd35Rectangles")]
        [TestCase(5, 5, 105, TestName = "OnSmallSizeAnd105Rectangles")]
        [TestCase(35, 35, 105, TestName = "OnBigSizeAnd105Rectangles")]
        public void PutNextRectangle_PutsRectanglesTightly(int sizeX, int sizeY, int amountOfRectangles)
        {
            layouter = new CircularCloudLayouter(new Point(0, 0));
            var size = new Size(sizeX, sizeY);
            for (var i = 0; i < amountOfRectangles - 1; i++)
                PutNextRectangle(size);
            var rect = PutNextRectangle(size);
            Math.Abs(rect.X).Should()
                .BeLessOrEqualTo(size.Width * amountOfRectangles / 4);
            Math.Abs(rect.Y).Should()
                .BeLessOrEqualTo(size.Height * amountOfRectangles / 4);
        }

        private Rectangle PutNextRectangle(Size size)
        {
            if (layouter == null)
                throw new NullReferenceException("Layouter was not initialized!");
            var rect = layouter.PutNextRectangle(size);
            generatedRectangles.Add(rect);
            return rect;
        }

        [Test]
        public void PutNextRectangle_Throws_WhenNegativeSize()
        {
            layouter = new CircularCloudLayouter(new Point(0, 0));
            Action action = () => PutNextRectangle(new Size(-10, 0));
            action.Should().Throw<ArgumentException>();
        }
    }
}