using System.Drawing;
using FluentAssertions;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests;

public class SpiralPointGenerator_Should
{
    private SpiralPointGenerator spiralPointGenerator;

    [SetUp]
    public void CreateSpiralGenerator()
    {
        spiralPointGenerator = new SpiralPointGenerator();
    }

    [Test]
    public void GetPointCalculatesCorrectUniqueCoordinates()
    {
        var existingPoints = new List<Point>();

        var expectedPoints = new Point[]
        {
            new (0, 0),
            new (1, 0),
            new (1, 1),
            new (0, 1),
            new (-1, 1),
            new (-1, 0),
            new (-1, -1),
            new (0, -1),
            new (1, -1),
            new (2, 0),
        };

        foreach (var expectedPoint in expectedPoints)
        {
            var actualPoint = spiralPointGenerator.GetNextPoint();
            while (existingPoints.Contains(actualPoint))
            {
                actualPoint = spiralPointGenerator.GetNextPoint();
            }
            existingPoints.Add(actualPoint);
            actualPoint.Should().Be(expectedPoint);
        }
    }
}