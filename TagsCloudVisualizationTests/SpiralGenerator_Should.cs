using System.Drawing;
using FluentAssertions;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests;

public class SpiralGenerator_Should
{
    private SpiralGenerator spiralGenerator;

    [SetUp]
    public void CreateSpiralGenerator()
    {
        spiralGenerator = new SpiralGenerator();
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
            var actualPoint = spiralGenerator.GetNextPoint();
            while (existingPoints.Contains(actualPoint))
            {
                actualPoint = spiralGenerator.GetNextPoint();
            }
            existingPoints.Add(actualPoint);
            actualPoint.Should().Be(expectedPoint);
        }
    }
}