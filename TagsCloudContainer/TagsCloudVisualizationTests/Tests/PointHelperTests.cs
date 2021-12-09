using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests.Tests
{
    public class PointHelperTests
    {
        [TestCaseSource(nameof(GetTopLeftCornerCases))]
        public Point GetTopLeftCorner_AssertResult(List<Point> points) => PointHelper.GetTopLeftCorner(points);

        public static IEnumerable<TestCaseData> GetTopLeftCornerCases()
        {
            yield return new TestCaseData(new List<Point> {new(1, 1), new(2, 2)})
                {ExpectedResult = new Point(1, 1), TestName = "With one point to the left and above"};

            yield return new TestCaseData(new List<Point> {new(0, 1), new(1, 0)})
                {ExpectedResult = new Point(0, 0), TestName = "With one point to the left and another above"};

            yield return new TestCaseData(new List<Point> {new(1, 4), new(2, 3), new(3, 2)})
                {ExpectedResult = new Point(1, 2), TestName = "With 3 positive points"};

            yield return new TestCaseData(new List<Point> {new(-1, -4), new(-2, -3), new(-3, -2)})
                {ExpectedResult = new Point(-3, -4), TestName = "With 3 negative points"};

            yield return new TestCaseData(new List<Point> {new(-1, 4), new(2, -5), new(-3, 2)})
                {ExpectedResult = new Point(-3, -5), TestName = "With 3 points"};
        }

        [TestCaseSource(nameof(GetBottomRightCornerCases))]
        public Point GetBottomRightCorner_AssertResult(List<Point> points) => PointHelper.GetBottomRightCorner(points);

        public static IEnumerable<TestCaseData> GetBottomRightCornerCases()
        {
            yield return new TestCaseData(new List<Point> {new(1, 1), new(2, 2)})
                {ExpectedResult = new Point(2, 2), TestName = "With one point to the right and below"};

            yield return new TestCaseData(new List<Point> {new(0, 1), new(1, 0)})
                {ExpectedResult = new Point(1, 1), TestName = "With one point to the right and another below"};

            yield return new TestCaseData(new List<Point> {new(1, 4), new(2, 3), new(3, 2)})
                {ExpectedResult = new Point(3, 4), TestName = "With 3 positive points"};

            yield return new TestCaseData(new List<Point> {new(-1, -4), new(-2, -3), new(-3, -2)})
                {ExpectedResult = new Point(-1, -2), TestName = "With 3 negative points"};

            yield return new TestCaseData(new List<Point> {new(1, -4), new(-2, 5), new(3, -2)})
                {ExpectedResult = new Point(3, 5), TestName = "With 3 points"};
        }
    }
}