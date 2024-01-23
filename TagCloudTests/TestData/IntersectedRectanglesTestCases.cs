using System.Drawing;

namespace TagCloudTests.TestData;

public class IntersectedRectanglesTestCases
{
    public static IEnumerable<TestCaseData> Data
    {
        get
        {
            yield return new TestCaseData(new Rectangle(1, 1, 1, 1), new Rectangle(1, 1, 1, 1))
                .SetName("WhenFirstEqualsSecond").Returns(true);
            yield return new TestCaseData(new Rectangle(1, 1, 1, 1), new Rectangle(0, 0, 3, 3))
                .SetName("WhenFirstInSecond").Returns(true);
            yield return new TestCaseData(new Rectangle(-1, 1, 2, 1), new Rectangle(0, 0, 3, 3))
                .SetName("WhenFirstEnterInSecond_ByLeftSide").Returns(true);
            yield return new TestCaseData(new Rectangle(1, 1, 2, 1), new Rectangle(0, 0, 3, 3))
                .SetName("WhenFirstEnterInSecond_ByRightSide").Returns(true);
            yield return new TestCaseData(new Rectangle(1, 1, 1, 2), new Rectangle(0, 0, 3, 3))
                .SetName("WhenFirstEnterInSecond_ByTopSide").Returns(true);
            yield return new TestCaseData(new Rectangle(1, -1, 1, 2), new Rectangle(0, 0, 3, 3))
                .SetName("WhenFirstEnterInSecond_ByBottomSide").Returns(true);
            yield return new TestCaseData(new Rectangle(0, 0, 2, 2), new Rectangle(1, 1, 2, 2))
                .SetName("WhenFirstRightTopAngle_IntersectWithSecondLeftBottomAngle").Returns(true);
            yield return new TestCaseData(new Rectangle(0, 1, 2, 2), new Rectangle(0, 1, 2, 2))
                .SetName("WhenFirstRightBottomAngle_IntersectWithSecondLeftTopAngle").Returns(true);
        }
    }
}