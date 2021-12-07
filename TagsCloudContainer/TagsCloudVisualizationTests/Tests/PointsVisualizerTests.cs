using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;
using TagsCloudVisualizationTests.TestingLibrary;

namespace TagsCloudVisualizationTests.Tests
{
    public class PointsVisualizerTests
    {
        [TestCaseSource(nameof(CalculateBitmapSizeCases))]
        public Size GetBitmapSize_AssertSize(List<Point> points)
        {
            var visualizer = new PointsVisualizer(points);
            TestContext.WriteLine("Points are:");
            TestContext.WriteLine(string.Join("\n", points.ToString()));
            return visualizer.GetBitmapSize();
        }

        public static IEnumerable<TestCaseData> CalculateBitmapSizeCases()
        {
            yield return new TestCaseData(new List<Point> {new(), new(3, 4)})
                {ExpectedResult = new Size(4, 5), TestName = "When second point is further"};

            yield return new TestCaseData(new List<Point> {new(3, 4), new()})
                {ExpectedResult = new Size(4, 5), TestName = "When first point is further"};

            yield return new TestCaseData(new List<Point> {new(0, 10), new(10, 0)})
                {ExpectedResult = new Size(11, 11), TestName = "When size determined by 2 points"};

            yield return new TestCaseData(new List<Point> {new(0, 10), new(10, 0), new(20, 0)})
                {ExpectedResult = new Size(21, 11), TestName = "With 3 points"};

            yield return new TestCaseData(new List<Point>())
                {ExpectedResult = new Size(1, 1), TestName = "Return 1x1 size when no points provided"};

            yield return new TestCaseData(new List<Point> {new(), new()})
                {ExpectedResult = new Size(1, 1), TestName = "Return 1x1 size when points are empty"};

            yield return new TestCaseData(new List<Point> {new(1, 0)})
                {ExpectedResult = new Size(1, 1), TestName = "Return 1x1 when one coordinate is 0"};

            yield return new TestCaseData(new List<Point> {new(), new(-10, -10)})
                {ExpectedResult = new Size(11, 11), TestName = "With one negative point"};

            yield return new TestCaseData(new List<Point> {new(5, 5), new(10, 10)})
                {ExpectedResult = new Size(6, 6), TestName = "Return smallest size with positive points"};

            yield return new TestCaseData(new List<Point> {new(-5, -5), new(-10, -10)})
                {ExpectedResult = new Size(6, 6), TestName = "Return smallest size with negative points"};
        }

        [TestCaseSource(nameof(DrawAssertBitmapCases))]
        public void Draw_AssertBitmap(List<Point> pointsToDraw, List<Point> expectedPoints, Size bitmapSize)
        {
            var visualizer = new PointsVisualizer(pointsToDraw);
            VisualizerTestHelper.AssertBitmap(visualizer, bitmapSize, expectedPoints, Color.Red);
        }

        public static IEnumerable<TestCaseData> DrawAssertBitmapCases()
        {
            var expectedPoints = new List<Point> {new(), new(1, 2)};
            var bitmapSize = new Size(2, 3);

            yield return new TestCaseData(new List<Point> {new(), new(1, 2)}, expectedPoints, bitmapSize)
                {TestName = "With positive points"};

            yield return new TestCaseData(new List<Point> {new(), new(-1, -2)}, expectedPoints, bitmapSize)
                {TestName = "With negative points"};
        }
    }
}