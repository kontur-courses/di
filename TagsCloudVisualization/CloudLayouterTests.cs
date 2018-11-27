using System.Drawing;
using System.Drawing.Imaging;
using NUnit.Framework;
using FluentAssertions;
using NUnit.Framework.Interfaces;

namespace TagsCloudVisualization
{
    [TestFixture]
    class CloudLayouterTests
    {
        private static Point center;
        private static CloudLayouter cloudLayouter;
        [SetUp]
        public void InitializeTests()
        {
            center = new Point(1000, 1000);
            var spiral = new ArchimedesSpiral(center);
            cloudLayouter = new CloudLayouter(spiral, center);
        }

        [TestCase(0, 0, TestName = "CenterOfCloudAreEquivalentToLeftUpperBoundOfFirstRectangleOnZeroSize")]
        [TestCase(2, 2, TestName = "CenterOfCloudAreEquivalentToLeftUpperBoundOfFirstRectangleOnSquareSize")]
        [TestCase(2, 4, TestName = "CenterOfCloudAreEquivalentToLeftUpperBoundOfFirstRectangleOnRectangleSize")]
        public void FirstRectangleAreInCenterOfTheCloud(int width, int height)
        {
            var firstRectangle = cloudLayouter.PutNextRectangle(new Size(width, height));
            var rectangleCenter = firstRectangle.GetCenter();

            rectangleCenter.ShouldBeEquivalentTo(center);
        }

        [Test]
        public void NextRectangleShouldByFartherOfCenter()
        {
            var rectanglesSizes = new [] { new Size(2, 2), new Size(2, 2) };

            var lastDistance = 0.0;

            foreach (var rectangleSize in rectanglesSizes)
            {
                var rectangle = cloudLayouter.PutNextRectangle(rectangleSize);
                var distance = rectangle.GetDistanceToPoint(center);

                distance.Should().BeGreaterOrEqualTo(lastDistance);

                lastDistance = distance;
            }
        }

        [Test]
        public void RectanglesAreNotIntersectingAfterAdditionNew()
        {
            var rectanglesSizes = new [] { new Size(100, 100), new Size(100, 100) };
            foreach (var rectangleSize in rectanglesSizes)
            {
                cloudLayouter.PutNextRectangle(rectangleSize);
            }

            foreach (var firstRectangleToCheck in cloudLayouter.Rectangles)
                foreach (var secondRectangleToCheck in cloudLayouter.Rectangles)
                {
                    if (firstRectangleToCheck == secondRectangleToCheck)
                        continue;
                    firstRectangleToCheck
                        .IntersectsWith(secondRectangleToCheck)
                        .Should().BeFalse();
                }
        }



        [TearDown]
        public static void SaveImageOfWrongTestCase()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed) return;
            
            var directory = TestContext.CurrentContext.TestDirectory;
            var path = $"{directory}\\..\\..\\Images\\FailedOn{TestContext.CurrentContext.Test.Name}.png";

            TestContext.Out.WriteLine($"Tag cloud visualization saved to file {path}");

            var bitmap = CircularCloudVisualization.GetBitmapWithRectangles(cloudLayouter);

            bitmap.Save(path, ImageFormat.Png);
        }
        //private IEnumerable TestCasesGeneratorFor_NextRectangleMustByFartherFromCenter()
        //{
        //    yield return new TestCaseData(new[] { new Size(2, 2), new Size(2, 2) })
        //        .SetName("AfterAdditionTwoEqualsRectangles");
        //}

        //[Test, TestCaseSource(nameof(TestCasesGeneratorFor_NextRectangleMustByFartherFromCenter))]
        //public void NextRectangleMustByFartherFromCenter(IEnumerable<Size> rectanglesSizes)
        //{
        //    var cloud = new CloudLayouter(new Point(0, 0));
        //    var lastDistance = 0.0;

        //    foreach (var rectangleSize in rectanglesSizes)
        //    {
        //        var rectangle = cloud.PutNextRectangle(rectangleSize);
        //        var distance = GetDistanceFromRectangleToPoint(rectangle, cloud);
        //        distance.Should().BeGreaterOrEqualTo(lastDistance);

        //        lastDistance = distance;
        //    }
        //}
    }
}
