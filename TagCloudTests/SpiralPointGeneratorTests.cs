using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagCloud;

namespace TagCloudTests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class SpiralPointGeneratorTests
{
    [Test]
    public void Generate_FirstPoint_AtCentre()
    {
        var center = new Point(0, 0);
        var generator = new SpiralPointGenerator(1, Math.PI / 3, 1);

        var firstPoint = generator.Generate(center).First();

        firstPoint.Should().BeEquivalentTo(center);
    }

    [Test]
    public void Generate_GeneratedLine_IntersectsCoordinateAxesAtCorrectPoints()
    {
        var generator = new SpiralPointGenerator(1, 45 / (180 / Math.PI), 1);

        var points = generator.Generate(new Point(0, 0)).Take(600).ToArray();
        var intersectXAxe = points
            .Where(p => p.Y == 0)
            .Select((p, i) => (x: p.X, r: i * 4));
        var intersectYAxe = points
            .Where(p => p.X == 0)
            .Select((p, i) => (y: p.Y, r: i * 4 - 2))
            .Skip(1);

        intersectXAxe.Should().AllSatisfy(t => Math.Abs(t.x).Should().Be(t.r));
        intersectYAxe.Should().AllSatisfy(t => Math.Abs(t.y).Should().Be(t.r));
    }
}