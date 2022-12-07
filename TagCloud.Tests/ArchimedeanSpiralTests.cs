using System.Drawing;
using TagCloud.Curves;

namespace TagCloud.Tests;

[TestFixture]
public class ArchimedeanSpiralTests
{
    [SetUp]
    public void SetUp()
    {
        _spiral = new ArchimedeanSpiral(0, 1);
    }

    public static IEnumerable<TestCaseData> Instance_IncorrectParameters
    {
        get
        {
            yield return new TestCaseData(-1, 1).SetName("startRadius < 0");
            yield return new TestCaseData(1, -1).SetName("extendRatio < 0");
            yield return new TestCaseData(1, 0).SetName("extendRatio == 0");
            yield return new TestCaseData(-1, -1).SetName("startRadius < 0 and extendRatio < 0");
            yield return new TestCaseData(-1, 0).SetName("startRadius < 0 and extendRatio == 0");
        }
    }

    [TestCaseSource(nameof(Instance_IncorrectParameters))]
    public void Instance_IncorrectParameters_ShouldFail(int startRadius, int extendRatio)
    {
        Action instantiating = () => new ArchimedeanSpiral(startRadius, extendRatio);
        instantiating.Should().Throw<ArgumentException>();
    }

    public static IEnumerable<TestCaseData> Instance_CorrectParameters
    {
        get
        {
            yield return new TestCaseData(1, 1).SetName("Start radius = 1, Extend ratio = 1");
            yield return new TestCaseData(0, 1).SetName("Start radius = 0, Extend ratio = 1");
            yield return new TestCaseData(2, 3).SetName("Start radius = 2, Extend ratio = 3");
        }
    }

    [TestCaseSource(nameof(Instance_CorrectParameters))]
    public void Instance_CorrectParameters_ShouldNotFail(int startRadius, int extendRatio)
    {
        Action instantiating = () => new ArchimedeanSpiral(startRadius, extendRatio);
        instantiating.Should().NotThrow<ArgumentException>();
    }

    private ArchimedeanSpiral _spiral;

    public static IEnumerable<TestCaseData> GetPointWithResults
    {
        get
        {
            yield return new TestCaseData(0).SetName("Zero angle should return start position")
                .Returns(new Point(0, 0));
            yield return new TestCaseData(Math.PI / 2).SetName("Pi/2 angle")
                .Returns(new Point(0, Convert.ToInt32(Math.PI / 2)));
            yield return new TestCaseData(Math.PI).SetName("Pi angle").Returns(new Point(-Convert.ToInt32(Math.PI), 0));
            yield return new TestCaseData(3 * Math.PI / 2).SetName("3*Pi/2 angle")
                .Returns(new Point(0, -Convert.ToInt32(3 * Math.PI / 2)));
            yield return new TestCaseData(2 * Math.PI).SetName("2*Pi angle")
                .Returns(new Point(Convert.ToInt32(2 * Math.PI), 0));
            yield return new TestCaseData(4 * Math.PI).SetName("4*Pi angle")
                .Returns(new Point(Convert.ToInt32(4 * Math.PI), 0));
        }
    }

    [TestCaseSource(nameof(GetPointWithResults))]
    public Point GetPoint_ShouldReturnCorrectPoint(double angle)
    {
        return _spiral.GetPoint(angle);
    }
}