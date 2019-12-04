using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using NUnit.Framework.Interfaces;
using System.IO;
using TagsCloudLayout;
using TagsCloudLayout.CloudLayouters;
using TagsCloudLayout.PointLayouters;

namespace TagsCloudApplicationTests
{
    [TestFixture]
    public class CloudLayouter_Should
    {
        private readonly Random rnd = new Random();
        private readonly int minWidth = 5;
        private readonly int maxWidth = 100;
        private readonly int minHeight = 5;
        private readonly int maxHeight = 100;
        private Point center;
        private CircularCloudLayouter layouter;
        private List<Rectangle> rectangles;
        private Size size;

        [SetUp]
        public void SetUp()
        {
            center = new Point(0, 0);
            layouter = new CircularCloudLayouter(new ArchimedeanSpiral(center));
            rectangles = new List<Rectangle>();
            size = CloudLayouterUtilities.GetRandomSize(5, 100, 5, 100);
        }

        [TestCase(10, 10)]
        [TestCase(25, 25)]
        public void PutNextRectangle_WithoutChangingSize(int width, int height)
        {
            var size = new Size(width, height);
            var rectangle = layouter.PutNextRectangle(size);
            rectangles.Add(rectangle);
            rectangle.Size.Should().BeEquivalentTo(size);
        }

        [TestCase(0, 0)]
        [TestCase(0, 10)]
        [TestCase(-10, 0)]
        public void ThrowArgumentException_OnNonPositiveSize(int width, int height)
        {
            Action act = () => layouter.PutNextRectangle(new Size(width, height));
            act.Should().Throw<ArgumentException>();
        }

        [TestCase(0, 0)]
        [TestCase(1000, 1000)]
        [TestCase(-1001, 1001)]
        public void PutFirstRectangle_NearCenter(int x, int y)
        {
            center = new Point(x, y);
            layouter = new CircularCloudLayouter(new ArchimedeanSpiral(center));
            var rectangle = layouter.PutNextRectangle(size);
            rectangles.Add(rectangle);

            rectangle.GetCenter().X.Should().BeApproximately(x, (float)size.Width / 2);
            rectangle.GetCenter().Y.Should().BeApproximately(y, (float)size.Height / 2);
        }

        [Test]
        public void PutRectanglesOnLayout_WithoutIntersection()
        {
            rectangles = CloudLayouterUtilities.GenerateRandomLayout(
                    center, rnd.Next(2, 25), minWidth, maxWidth, minHeight, maxHeight);
            foreach(var rectangle in rectangles)
                rectangles
                    .Any(rect => rect.IntersectsWith(rectangle) && rect != rectangle)
                    .Should()
                    .BeFalse("rectangles should not intersect!");
        }

        [TestCase(0, 0)]
        [TestCase(100, -100)]
        public void PutRectangles_DenselyAroundCenter(int x, int y)
        {
            center = new Point(x, y);
            layouter = new CircularCloudLayouter(new ArchimedeanSpiral(center));

            rectangles = CloudLayouterUtilities.GenerateRandomLayout(
                    center, rnd.Next(25, 50), minWidth, maxWidth, minHeight, maxHeight);
            var totalMass = rectangles.Select(rect => rect.Width * rect.Height).Sum();

            var maxSquaredDistance = center.GetMaxSquaredDistanceTo(rectangles);
            var circleSize = Math.PI * maxSquaredDistance;
            var emptyArea = circleSize - totalMass;
            emptyArea.Should().NotBeInRange(circleSize / 2, double.MaxValue, 
                "more than half of the minimum circular area containing all" +
                "of the rectangles should not be empty");

        }

        [TearDown]
        public void TearDown()
        {
            var testContext = TestContext.CurrentContext;
            if (testContext.Result.Outcome.Status == TestStatus.Passed)
                return;
            var bitmap = RectangleVisualizator.GetBitmapFromRectangles(center, rectangles);
            var testImageDirectory = Path.Combine(testContext.WorkDirectory, "failed");
            var testImagePath = Path.Combine(testImageDirectory, $"{testContext.Test.FullName}.bmp");
            Directory.CreateDirectory(testImageDirectory);
            bitmap.Save(testImagePath);

            TestContext.WriteLine($"Tag cloud visualization saved to file {testImagePath}");
        }
    }
}
