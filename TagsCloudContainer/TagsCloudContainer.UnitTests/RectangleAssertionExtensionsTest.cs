using System.Drawing;

namespace TagsCloudContainer.UnitTests;

[TestFixture]
public class RectangleAssertionExtensionsTest
{
    public static IEnumerable<TestCaseData> Rectangles()
    {
        yield return new(new Rectangle(0, 0, 10, 10),
            new Rectangle(10, 0, 10, 10), true);
        yield return new(new Rectangle(0, 0, 10, 10),
            new Rectangle(20, 0, 10, 10), false);
        yield return new(new Rectangle(0, 0, 10, 10),
            new Rectangle(9, 0, 10, 10), false);
    }

    [TestCaseSource(nameof(Rectangles))]
    public void TouchesWith_Expected_WithParameters(Rectangle a, Rectangle b, bool expected)
    {
        a.TouchesWith(b).Should().Be(expected);
    }
}