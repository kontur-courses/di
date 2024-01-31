using System.Drawing;
using NUnit.Framework;

namespace TagCloudDi_Tests
{
    public class TestDataCircularCloudLayouter
    {
        public static IEnumerable<TestCaseData> ZeroOrLessHeightOrWidth_Size()
        {
            yield return new TestCaseData(new Size(0, 1)).SetName("Zero_Width_Size");
            yield return new TestCaseData(new Size(1, 0)).SetName("Zero_Height_Size");
            yield return new TestCaseData(new Size(0, 0)).SetName("Zero_Width_And_Height_Size");
            yield return new TestCaseData(new Size(int.MinValue, 1)).SetName("Negative_Width_Size");
            yield return new TestCaseData(new Size(1, int.MinValue)).SetName("Negative_Height_Size");
            yield return new TestCaseData(new Size(int.MinValue, int.MinValue))
                .SetName("Negative_Width_And_Height_Size");
        }
    }
}
