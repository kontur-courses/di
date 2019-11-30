using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using static TagsCloudVisualization.Tests.CircularCloudLayouter_Tests_AuxiliaryTools;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class CircularCloudLayouter_Tests
    {
        private CircularCloudLayouter cloud;
        private List<Rectangle> rectangles;
        private Point center;

        [SetUp]
        public void SetUp()
        {
            center = new Point(500, 500);
            cloud = new CircularCloudLayouter(center);
            rectangles = new List<Rectangle>();
        }

        [TestCase(0, 0)]
        [TestCase(-5, 1)]
        [TestCase(10, -78)]
        [TestCase(-100, -100)]
        public void PutNextRectangle_InvalidSize_ShouldThrowException(int width, int height)
        {
            Action act = () => cloud.PutNextRectangle(new Size(width, height));
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void PutNextRectangle_FirstRectangle_ShouldBeInCenter()
        {
            var rectangle = cloud.PutNextRectangle(new Size(100, 50));
            rectangle.Location.Should().Be(new Point(450, 475));
        }

        [TestCase(10, 15, 15, TestName = "10EqualRectangles")]
        [TestCase(100, 15, 15, TestName = "100EqualRectangles")]
        [TestCase(500, 15, 15, TestName = "500EqualRectangles")]
        public void CreateCloudWithEqualRectangles_ValidValues_ShouldCreateCloud(int rectanglesCount, int width,
            int height)
        {
            var rectangles = GetCloudWithEqualRectangles(rectanglesCount, width, height);
            rectangles.Count.Should().Be(rectanglesCount);
        }

        [TestCase(10, 10, 50, 10, 50, TestName = "10DifferentRectangles")]
        [TestCase(100, 10, 50, 10, 50, TestName = "100DifferentRectangles")]
        [TestCase(500, 10, 50, 10, 50, TestName = "500DifferentRectangles")]
        public void CreateCloudWithDifferentRectangles_ValidValues_ShouldCreateCloud(int rectanglesCount, int minWidth,
            int maxWidth, int minHeight, int maxHeight)
        {
            var rectangles = GetCloudWithDifferentRectangles(rectanglesCount, minWidth, maxWidth, minHeight, maxHeight);
            rectangles.Count.Should().Be(rectanglesCount);
        }

        [TestCase(100, 10, 50, 10, 50)]
        [TestCase(300, 15, 20, 10, 60)]
        [TestCase(500, 10, 20, 20, 30)]
        public void CircularCloudLayouter_CloudWidthAndHeight_ShouldBeSimilar(int rectanglesCount, int minWidth,
            int maxWidth, int minHeight, int maxHeight)
        {
            var rectangles = GetCloudWithDifferentRectangles(rectanglesCount, minWidth, maxWidth, minHeight, maxHeight);
            var cloudWidth = GetCloudWidth(rectangles);
            var cloudHeight = GetCloudHeight(rectangles);
            Math.Abs(cloudWidth - cloudHeight).Should().BeLessOrEqualTo(Math.Max(maxHeight, maxWidth));
        }

        [TestCase(100, 15, 25, 15, 25)]
        [TestCase(100, 10, 50, 10, 50)]
        [TestCase(300, 15, 20, 10, 60)]
        [TestCase(500, 10, 20, 20, 30)]
        public void CircularCloudLayouter_Rectangles_ShouldFormCircle(int rectanglesCount, int minWidth, int maxWidth,
            int minHeight, int maxHeight)
        {
            var rectangles = GetCloudWithDifferentRectangles(rectanglesCount, minWidth, maxWidth, minHeight, maxHeight);
            var cloudRadius = GetCloudRadius(rectangles);
            var rectanglesOutOfCircleCount = rectangles.Count(rectangle =>
                rectangle.Location.GetDistanceToPoint(center) > cloudRadius);

            rectanglesOutOfCircleCount.Should().BeLessOrEqualTo((int) (rectanglesCount * 0.03));
        }
        
        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed)
                return;
            var testName = TestContext.CurrentContext.Test.Name;
            var painter = new Painter(new Size(1000, 1000));
            var image = painter.GetMultiColorCloud(rectangles);
            var fileName = new StringBuilder(testName).Append("FAILED").ToString();
            var path = ImageSaver.SaveImageToDefaultDirectory(fileName, image);
            Console.WriteLine($"Tag cloud visualization saved to file {path}");
        }
    }
}