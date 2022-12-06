using System.Drawing;

namespace TagsCloud.Tests
{
    public class TestData
    {
        public static TestCaseData[] DefaultPointsAndSizeForPlace =
        {
            new TestCaseData(0, 0, 4, 4).Returns(new Rectangle(new Point(-2, -2), new Size(4, 4))),
            new TestCaseData(-2, -2, 4, 4).Returns(new Rectangle(new Point(-4, -4), new Size(4, 4))),
            new TestCaseData(0, 0, 10, 10).Returns(new Rectangle(new Point(-5, -5), new Size(10, 10))),
        };

        public static TestCaseData[] IncorrectSize =
        {
            new TestCaseData(0, 1).SetName("Zero width"),
            new TestCaseData(1, 0).SetName("Zero height"),
            new TestCaseData(-2, 1).SetName("Negative width"),
            new TestCaseData(1, -2).SetName("Negative height"),
        };

        public static TestCaseData[] CorrectSizes =
        {
            new(new[] {3, 3, 2}, new[] {2, 1, 4}),
            new(new[] {1, 2, 4}, new[] {10, 10, 10})
        };

        public static TestCaseData[] IncorrectStepCount =
        {
            new TestCaseData(0).SetName("Set zero in Step"),
            new TestCaseData(-5).SetName("Set negative number in Step")
        };
            
        public static TestCaseData[] CorrectStepCount = {
            new TestCaseData(1).SetName("Set one to Steps"),
            new TestCaseData(25).SetName("Set 25 to Steps")
        };
    }
}