using System.Drawing;
using TagCloud.Curves;

namespace TagCloud.Tests;

[TestFixture]
public class CloudLayouterTests
{
    
    [SetUp] 
    public void SetUp()
    {
        _curve = new ArchimedeanSpiral();
        _layouter = new CloudLayouter();
    }

    private CloudLayouter _layouter;
    private ICurve _curve;

    private void PutAllRectangles(IReadOnlyList<Size> rectangles)
    {
        foreach (var rectangleSize in rectangles)
            _layouter.PutRectangle(_curve, rectangleSize);
    }

    private static IEnumerable<TestCaseData> SizesOfRectangles
    {
        get
        {
            yield return new TestCaseData(
                new List<Size>
                {
                    new(100, 100),
                    new(100, 100),
                    new(100, 100),
                    new(100, 100),
                    new(100, 100)
                });
            yield return new TestCaseData(
                new List<Size>
                {
                    new(100, 100),
                    new(200, 100),
                    new(100, 200),
                    new(1000, 1000),
                    new(100, 100)
                });
        }
    }

    [TestCaseSource(nameof(SizesOfRectangles))]
    public void PutNextRectangle_ShouldNotMakeIntersections(IReadOnlyList<Size> sizesOfRectangles)
    {
        PutAllRectangles(sizesOfRectangles);

        foreach (var rectangle1 in _layouter.Rectangles)
        foreach (var rectangle2 in _layouter.Rectangles)
        {
            if (rectangle1 == rectangle2)
                continue;
            rectangle1.IntersectsWith(rectangle2).Should().BeFalse();
        }
    }
}