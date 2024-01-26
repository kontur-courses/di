using Aspose.Drawing;
using TagCloud.Domain.Layout;

namespace TagCloud.Tests;

public class LayoutTests
{
    [SetUp]
    public void SetUp()
    {
        layout = new Layout();
    }
    
    private Layout layout;
    
    [Test]
    public void GetNextCoord_GivesLayerByLayerCoords()
    {
        var coords = layout.GetNextCoord(new Point(0, 0)).Take(10);

        coords
            .Should()
            .BeEquivalentTo(new[]
            {
                new Point(0, 0),
                new Point(-1, -1),
                new Point(-1, 0),
                new Point(-1, 1),
                new Point(0, -1),
                new Point(0, 1),
                new Point(1, -1),
                new Point(1, 0),
                new Point(1, 1),
                new Point(-2, -2),
            });
    }
    
    [Test, Timeout(50)]
    public void GetNextCoord_MustBeEfficient()
    {
        layout.GetNextCoord(new Point(0, 0))
            .Take(30_000)
            .ToArray();
    }
}