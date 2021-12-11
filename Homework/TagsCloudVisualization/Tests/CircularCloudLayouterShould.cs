using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class CircularCloudLayouterShould
    {
        private CircularCloudLayouter _testLayout;

        [TearDown]
        public void SaveLayout()
        {
            var testResult = TestContext.CurrentContext.Result.Outcome.Status;
            if (testResult != TestStatus.Failed || _testLayout == null) return;
            var testName = TestContext.CurrentContext.Test.Name;
            var testDirectory = TestContext.CurrentContext.TestDirectory + "\\";
            var pathToImage = testDirectory + testName + ".bmp";
            CloudLayouterPainter.Draw(_testLayout,pathToImage);
            var message = $"Tag cloud visualization saved to file {pathToImage}";
            TestContext.Out.WriteLine(message);
        }

        [Test]
        public void InitializeFieldsAfterInstanceCreation()
        {
            var center = new Point(10, 10);

            var layouter = new CircularCloudLayouter(center);
            _testLayout = layouter;

            layouter.Rectangles.Should().NotBeNull();
            layouter.CloudCenter.Should().Be(center);
        }

        [TestCase(-1, 20, TestName = "width or height is negative")]
        [TestCase(10, 0, TestName = "width or height equals zero")]
        public void ThrowExceptionWhenRectangleSizeIsIncorrect(int width, int height)
        {
            var rectangleSize = new Size(-1, 20);
            var center = new Point(10, 10);
            var layouter = new CircularCloudLayouter(center);
            _testLayout = layouter;
            void Act() => layouter.PutNextRectangle(rectangleSize);

            FluentActions.Invoking(Act).Should().Throw<ArgumentException>();
        }

        [Test]
        public void PutFirstRectangleInTheCloudCenter()
        {
            var rectangleSize = new Size(100, 100);
            var center = new Point(0, 0);
            var layouter = new CircularCloudLayouter(center);
            _testLayout = layouter;
            var expectedLocation = new Point(-50, -50);

            var firstRectangle = layouter.PutNextRectangle(rectangleSize);

            firstRectangle.Location.Should().Be(expectedLocation);
        }

        [Test]
        [Repeat(10)]
        public void MakeCloudCircleDeviationLessThanTwentyFivePercent()
        {
            var center = new Point(200, -200);
            var layouter = new CircularCloudLayouter(center);

            PutRandomRectangles(layouter, 500);
            _testLayout = layouter;
            var cloudConvexHull = layouter.GetCloudConvexHull();
            var (minLength, maxLength) = GetMinMaxHullVectorsLengths(center, cloudConvexHull);
            var deviation = GetCloudDeviation(minLength, maxLength);

            deviation.Should().BeLessOrEqualTo(0.25);
        }

        [Test]
        [Repeat(10)]
        public void MakeCloudDensityDeviationLessThanThirtyPercent()
        {
            var center = new Point(750, 750);
            var layouter = new CircularCloudLayouter(center);

            PutRandomRectangles(layouter, 500);
            _testLayout = layouter;
            var enclosingCircleRadius = layouter.EnclosingRadius;
            var enclosingCircleArea = Math.PI * enclosingCircleRadius * enclosingCircleRadius;
            var cloudArea = layouter.Rectangles.Sum(rect => rect.Width * rect.Height);
            var deviation = GetCloudDeviation(cloudArea, enclosingCircleArea);

            deviation.Should().BeLessOrEqualTo(0.3);
        }

        [Test]
        public void PutRectanglesWithousIntersects()
        {
            var center = new Point(0, 0);
            var layouter = new CircularCloudLayouter(center);

            PutRandomRectangles(layouter, 100);
            _testLayout = layouter;

            AreRectanglesIntersected(layouter).Should().BeFalse();
        }

        private static bool AreRectanglesIntersected(CircularCloudLayouter layouter)
        {
            var rectangles = layouter.Rectangles.ToList();
            for (var i = 0; i < rectangles.Count; i++)
                for (var j = i + 1; j < rectangles.Count; j++)
                    if (rectangles[i].IntersectsWith(rectangles[j]) && rectangles[i] != rectangles[j])
                        return true;

            return false;
        }

        private static void PutRandomRectangles(CircularCloudLayouter layouter, int rectanglesCount)
        {
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            const int minWidth = 10;
            const int maxWidth = 70;
            const int minHeight = 10;
            const int maxHeight = 35;

            for (var i = 0; i < rectanglesCount; i++)
                layouter.PutNextRectangle(
                    new Size(rnd.Next(minWidth, maxWidth), rnd.Next(minHeight, maxHeight)));
        }

        private static double GetCloudDeviation(double cloudValue, double deviateFrom)
        {
            return 1 - Math.Abs(cloudValue / deviateFrom);
        }

        private static (double minLength, double maxLength) GetMinMaxHullVectorsLengths(
            Point center, IReadOnlyCollection<Point> hull)
        {
            var hullVectorsLengths = hull
                .Select(point => point.GetDistanceTo(center))
                .ToArray();
            return (hullVectorsLengths.Min(), hullVectorsLengths.Max());
        }
    }
}
