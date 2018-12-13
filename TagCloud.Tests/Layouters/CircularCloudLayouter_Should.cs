using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using TagCloud.Core.Layouters;
using TagCloud.Core.Settings.DefaultImplementations;
using TagCloud.Tests.Extensions;

namespace TagCloud.Tests.Layouters
{
    public class CircularCloudLayouter_Should
    {
        private List<RectangleF> resRectangles;
        private CircularCloudLayouter layouter;

        [SetUp]
        public void SetUp()
        {
            resRectangles = new List<RectangleF>();
            layouter = new CircularCloudLayouter(new LayoutingSettings());
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Passed)
            {
                var directory = TestContext.CurrentContext.TestDirectory;
                var filename = TestContext.CurrentContext.Test.Name;
                var path = $"{directory}\\{filename}.png";
                var bitmap = RectanglesVisualizer.Visualize(resRectangles);
                bitmap.Save(path);
                TestContext.WriteLine($"Tag cloud visualization saved to file {path}");
            }
        }

        [Test]
        public void ReturnRectangleLocatedInTheCenter_OnTheFirstCallOfPutNextRectangleMethod()
        {
            var centerPoint = new PointF(0, 0);
            layouter.RefreshWith(centerPoint);

            var firstRectangle = layouter.PutNextRectangle(new Size(10, 10));

            firstRectangle.Location.Should().Be(centerPoint);
        }

        [TestCase(10, -10, TestName = "With negative height")]
        [TestCase(10, 0, TestName = "With zero height")]
        [TestCase(-10, 10, TestName = "With negative width")]
        [TestCase(0, 10, TestName = "With zero width")]
        public void ThrowArgumentException_OnPuttingInvalidRectangle(int width, int height)
        {
            layouter.RefreshWith(new PointF(0, 0));
            Action putIncorrectSizeAction = () => layouter.PutNextRectangle(new Size(width, height));
            putIncorrectSizeAction.Should().Throw<ArgumentException>();
        }

        [Test]
        public void ReturnNonIntersectingRectangles()
        {
            const int rectanglesCount = 100;
            layouter.RefreshWith(new PointF(1, -1));
            var rectangleSize = new Size(10, 10);

            for (int i = 0; i < rectanglesCount; i++)
                resRectangles.Add(layouter.PutNextRectangle(rectangleSize));

            for (int i = 0; i < rectanglesCount; i++)
            for (int j = i + 1; j < rectanglesCount; j++)
                resRectangles[i].IntersectsWith(resRectangles[j]).Should().BeFalse();
        }

        [Test]
        public void ReturnDifferentRectangles()
        {
            const int rectanglesCount = 100;
            layouter.RefreshWith(new PointF(1, 1));
            var rectangleSize = new Size(10, 10);

            for (int i = 0; i < rectanglesCount; i++)
                resRectangles.Add(layouter.PutNextRectangle(rectangleSize));

            for (int i = 0; i < rectanglesCount; i++)
            for (int j = i + 1; j < rectanglesCount; j++)
                resRectangles[i].Should().NotBe(resRectangles[j]);
        }

        [Test]
        public void PutNewRectanglesInsideTheCircleWithRadius25_AfterPutting100Rectangles3x4()
        {
            const int rectanglesCount = 100;
            const int radius = 25;
            var center = new Point(-2, 2);
            layouter.RefreshWith(new PointF());
            var rectangleSize = new Size(3, 4);

            for (int i = 0; i < rectanglesCount; i++)
                resRectangles.Add(layouter.PutNextRectangle(rectangleSize));

            foreach (var rectangle in resRectangles)
                rectangle.Location.GetDistanceTo(center).Should().BeLessThan(radius);
        }

        [Test, Timeout(500)]
        public void Put1000RectanglesIn500Milliseconds()
        {
            const int rectanglesCount = 1000;
            layouter.RefreshWith(new PointF(1, -1));
            var rectangleSize = new Size(10, 10);

            for (int i = 0; i < rectanglesCount; i++)
                layouter.PutNextRectangle(rectangleSize);
        }
    }
}