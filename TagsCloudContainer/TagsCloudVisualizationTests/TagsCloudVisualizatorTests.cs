using TagsCloudVisualization.CloudLayouters;
using TagsCloudVisualization.PointsProviders;

namespace TagsCloudVisualizationTests;

public class TagsCloudVisualizatorTests
{
    [Test]
    public void Constructor_ThrowsException_WhenLayouterContainsZeroElements()
    {
        var layouter = new CircularCloudLayouter(new ArchimedeanSpiralPointsProvider(Point.Empty));
        var a = () => new TagsCloudVisualizator(layouter);

        a.Should().Throw<ArgumentException>();
    }
}