using System.Drawing;
using TagCloud.Extensions;
using TagCloudTests.TestData;

namespace TagCloudTests;

public class HasIntersectedRectangleTests
{
    [TestCaseSource(typeof(IntersectedRectanglesTestCases), nameof(IntersectedRectanglesTestCases.Data))]
    [TestCaseSource(typeof(NotIntersectedRectanglesTestCases), nameof(NotIntersectedRectanglesTestCases.Data))]
    public bool ReturnValue(Rectangle first, Rectangle second)
    {
        return new[] { first, second }.HasIntersectedRectangles()
            && new[] { second, first }.HasIntersectedRectangles();
    }

    [Test]
    public void ReturnFalse_ThenConainsOneElement()
    {
        new[] { new Rectangle(0, 0, 1, 1) }.HasIntersectedRectangles().Should().BeFalse();
    }
    
    [Test]
    public void ReturnFalse_ThenEmpty()
    {
        Array.Empty<Rectangle>().HasIntersectedRectangles().Should().BeFalse();
    }
}