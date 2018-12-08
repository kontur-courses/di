using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests
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
            cloudLayouter = new CloudLayouter(spiral);
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
            var rectanglesSizes = new[] { new Size(2, 2), new Size(2, 2) };

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
            var rectanglesSizes = new[] { new Size(100, 100), new Size(100, 100) };
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

            var visualizer = new TagCloudVisualization(cloudLayouter);
            var rectangles = cloudLayouter.Rectangles;
            visualizer.SaveRectanglesCloud(
                $"FailedOn{TestContext.CurrentContext.Test.Name}",
                TestContext.CurrentContext.TestDirectory, 
                rectangles, 
                center);
        }

        private IEnumerable<TestCaseData> TestCasesGeneratorFor_NextRectangleMustByFartherFromCenter()
        {
            yield return new TestCaseData(new[] { new Size(2, 2), new Size(2, 2) })
                .SetName("AfterAdditionTwoEqualsRectangles");
        }

        [Ignore("Not implemented")]
        [Test, TestCaseSource(nameof(TestCasesGeneratorFor_NextRectangleMustByFartherFromCenter))]
        public void NextRectangleMustByFartherFromCenter(IEnumerable<Size> rectanglesSizes)
        {
            var center = new Point(0, 0);
            var cloud = new CloudLayouter(new ArchimedesSpiral(center));
            var lastDistance = 0.0;

            foreach (var rectangleSize in rectanglesSizes)
            {
                var rectangle = cloud.PutNextRectangle(rectangleSize);
                var distance = rectangle.GetDistanceToPoint(center);
                distance.Should().BeGreaterOrEqualTo(lastDistance);

                lastDistance = distance;
            }
        }
    }
}
