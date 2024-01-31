using System;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using TagsCloudVisualization;
using FluentAssertions;
using NUnit.Framework.Interfaces;
using TestContext = NUnit.Framework.TestContext;
using System.IO;
using System.Reflection;
using TagsCloudVisualization.PointDistributors;

namespace TagsCloudVisualizationTest
{
    [TestFixture]
    public class CircularCloudLayouterTests
    {
      //  private readonly CircularCloudLayouter layouter = new CircularCloudLayouter(new Spiral(new Point(0,0), 1, 0.1));
        private static readonly Point Center = new Point(50, 50);
        private RectangleF[] currentRectangles;

        [TearDown]
        public void TearDown()
        {
            if (currentRectangles == null ||
                TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed)
                return;

            var cloudCreator = new TagCloudRenderer();
            var settings = new VisualizingSettings(TestContext.CurrentContext.Test.Name, new Size(100, 100));
            cloudCreator.DrawCloud(currentRectangles, settings);

            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            Console.WriteLine($"Tag cloud visualization saved to {path}\\{settings.ImageName}");
        }

        [Test]
        public static void CircularCloudLayouterCtor_WhenPassValidArguments_DoesNotThrowException() =>
            Assert.DoesNotThrow(() => new CircularCloudLayouter(new Spiral(new Point(0,0), 1, 0.1)));

        public static TestCaseData[] InvalidArguments =
        {
            new TestCaseData(-200, 200).SetName("PassNegativeWidth"),
            new TestCaseData(200, -200).SetName("PassNegativeHeight"),
            new TestCaseData(-200, -200).SetName("PassNegativeArguments"),
            new TestCaseData(0, 0).SetName("PassOnlyZero")
        };

        [TestOf(nameof(CircularCloudLayouter.PutNextRectangle))]
        [TestCaseSource(nameof(InvalidArguments))]
        public void WhenPassInvalidArguments_ShouldThrowArgumentException(int width, int height)
        {
            CircularCloudLayouter layouter = new CircularCloudLayouter(new Spiral(new Point(width/2, height/2), 1, 0.1));

            Assert.Throws<ArgumentException>(() => layouter.PutNextRectangle(new Size(width, height)));
        }
           

        [Test]
        public void WhenPutNewRectangle_ShouldBeAddedToList()
        {
            CircularCloudLayouter layouter = new CircularCloudLayouter(new Spiral(new Point(0, 0), 1, 0.1));

            currentRectangles = new RectangleF[]
            {
                layouter.PutNextRectangle(new Size(40, 20))
            };

            currentRectangles.Length.Should().Be(1);
        }

        public static TestCaseData[] RectanglesPosition =
        {
            new TestCaseData(new Size(40, 20), new Size(40, 20))
                .Returns(false).SetName("WhenPassIdenticalRectangles"),
            new TestCaseData(new Size(60, 20), new Size(40, 20))
                .Returns(false).SetName("WhenPassRectanglesOfDifferentSizes")
        };

        [TestOf(nameof(CircularCloudLayouter.PutNextRectangle))]
        [TestCaseSource(nameof(RectanglesPosition))]
        public bool WhenPassSeveralRectangles_ShouldReturnCorrectIntersectionResult(Size rectangleSize, Size newRectangleSize)
        {
            CircularCloudLayouter layouter = new CircularCloudLayouter(new Spiral(new Point(0, 0), 1, 0.1));
            currentRectangles = new RectangleF[]
            {
                layouter.PutNextRectangle(rectangleSize),
                layouter.PutNextRectangle(newRectangleSize)
            };

            return currentRectangles.First().IntersectsWith(currentRectangles.Last());
        }

        [Test]
        [TestOf(nameof(CircularCloudLayouter.PutNextRectangle))]
        public void WhenPassFirstPoint_ShouldBeInCenter()
        {
            CircularCloudLayouter layouter = new CircularCloudLayouter(new Spiral(new Point(500, 500), 1, 0.1));

            currentRectangles = new RectangleF[]
            {
                layouter.PutNextRectangle(new Size(40, 20))
            };

            currentRectangles.First().Location.X.Should().Be(480);
            currentRectangles.First().Location.Y.Should().Be(490);
        }
    }
}