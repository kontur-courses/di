using System.Drawing;
using FluentAssertions;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests;

public class PolarPoint_Should
{
    [Test]
    public void CastsToPointCorrectly()
    {
        var polarPoint = new PolarPoint(2.5, 1.2);
        ((Point)polarPoint).Should().Be(new Point(1, 2));
    }
}