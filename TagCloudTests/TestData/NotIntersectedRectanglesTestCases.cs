using System.Drawing;

namespace TagCloudTests.TestData;

public class NotIntersectedRectanglesTestCases
{
    public static IEnumerable<TestCaseData> Data
    {
        get
        {
            yield return new TestCaseData(new Rectangle(0, 0, 1, 1), new Rectangle(1, 0, 1, 1))
                .SetName("WhenFirstRightBorder_СoncidesWithSecondLeftBorder").Returns(false);
            yield return new TestCaseData(new Rectangle(0, 0, 1, 1), new Rectangle(0, 1, 1, 1))
                .SetName("WhenFirstTopBorder_СoncidesWithSecondBottomBorder").Returns(false);
        }
    }
}