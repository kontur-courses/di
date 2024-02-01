using System.Drawing;
using TagsCloudPainter.FormPointer;
using TagsCloudPainter.Settings.Cloud;
using TagsCloudPainter.Settings.FormPointer;

namespace TagsCloudPainterTests;

[TestFixture]
public class ArchimedeanSpiralTests
{
    private static IEnumerable<TestCaseData> ConstructorArgumentException => new[]
    {
        new TestCaseData(new Point(1, 1), 0, 1, 1).SetName("WhenGivenNotPositiveStep"),
        new TestCaseData(new Point(1, 1), 1, 0, 1).SetName("WhenGivenNotPositiveRadius"),
        new TestCaseData(new Point(1, 1), 1, 1, 0).SetName("WhenGivenNotPositiveAngle")
    };

    [TestCaseSource(nameof(ConstructorArgumentException))]
    public void Constructor_ShouldThrowArgumentException(Point center, double step, double radius, double angle)
    {
        var cloudSettings = new CloudSettings { CloudCenter = center };
        var pointerSettings = new SpiralPointerSettings { AngleConst = angle, RadiusConst = radius, Step = step };
        Assert.Throws<ArgumentException>(() => new ArchimedeanSpiralPointer(cloudSettings, pointerSettings));
    }
}