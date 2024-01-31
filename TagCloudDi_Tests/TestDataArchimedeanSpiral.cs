using System.Drawing;
using NUnit.Framework;

namespace TagCloudDi_Tests
{
    public class TestDataArchimedeanSpiral
    {
        public static IEnumerable<TestCaseData> Different_CenterPoints()
        {
            yield return new TestCaseData(new Point(0, 0)).SetName("(0, 0) center");
            yield return new TestCaseData(new Point(343, 868)).SetName("(343, 868) center");
            yield return new TestCaseData(new Point(960, 540)).SetName("(960, 540) center");
        }

        public static IEnumerable<TestCaseData> DifferentIterationsAdded_ExpectedPoints()
        {
            yield return new TestCaseData(0, new Point(0, 0)).SetName("0 iterations, central point");
            yield return new TestCaseData(90, new Point(0, (int)(Math.PI / 2))).SetName("90 iterations, half PI");
            yield return new TestCaseData(180, new Point((int)(-Math.PI), 0)).SetName("180 iterations, PI");
            yield return new TestCaseData(270, new Point(0, (int)(-Math.PI * 3 / 2))).SetName("270 iterations, 3/2 PI");
            yield return new TestCaseData(360, new Point((int)(2 * Math.PI), 0)).SetName("360 iterations, double PI");
            yield return new TestCaseData(450, new Point(0, (int)(Math.PI * 5 / 2))).SetName("450 iterations, 5/2 PI");
            yield return new TestCaseData(540, new Point((int)(-3 * Math.PI), 0)).SetName("540 iterations, triple PI");
        }
    }
}
