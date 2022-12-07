using System.Drawing;
using TagCloud.Extensions;

namespace TagCloud.Tests;

[TestFixture]
public class ExtensionsTests
{
    public static IEnumerable<TestCaseData> SizeMultiply
    {
        get
        {
            yield return new TestCaseData(new Size(0, 0), 2).SetName("Zero size * 2 = Zero size")
                .Returns(new Size(0, 0));
            yield return new TestCaseData(new Size(0, 0), -1).SetName("Zero size * (-1) = Zero size")
                .Returns(new Size(0, 0));
            yield return new TestCaseData(new Size(1, 1), 2).SetName("(1, 1) * 2 = (2, 2)").Returns(new Size(2, 2));
            yield return new TestCaseData(new Size(1, 1), -1).SetName("(1, 1) * (-1) = (-1, -1)")
                .Returns(new Size(-1, -1));
            yield return new TestCaseData(new Size(2, 3), 2).SetName("(2, 3) * 2 = (4, 6)").Returns(new Size(4, 6));
            yield return new TestCaseData(new Size(2, 2), 2.5).SetName("(2, 2) * 2.5 = (5, 5)").Returns(new Size(5, 5));
            yield return new TestCaseData(new Size(3, 3), 2.5).SetName("(3, 3) * 2.5 = (7, 7)").Returns(new Size(7, 7));
        }
    }

    [TestCaseSource(nameof(SizeMultiply))]
    public Size Size_MultiplyByDouble(Size size, double number)
    {
        return size.Multiply(number);
    }

    public static IEnumerable<TestCaseData> CentersOfRectangles
    {
        get
        {
            yield return new TestCaseData(new Rectangle(0, 0, 100, 100))
                .SetName("start = (0, 0), size = (100, 100) => center = (50, 50)").Returns(new Point(50, 50));

            yield return new TestCaseData(new Rectangle(100, 100, 100, 100))
                .SetName("start = (100, 100), size = (100, 100) => center = (150, 150)").Returns(new Point(150, 150));

            yield return new TestCaseData(new Rectangle(100, 100, 1000, 1000))
                .SetName("start = (100, 100), size = (1000, 1000) => center = (600, 600)").Returns(new Point(600, 600));
        }
    }

    [TestCaseSource(nameof(CentersOfRectangles))]
    public Point GetCenterOfRectangle(Rectangle rectangle)
    {
        return rectangle.GetCenter();
    }
}