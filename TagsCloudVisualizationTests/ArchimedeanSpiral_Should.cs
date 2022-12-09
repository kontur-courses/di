using System.Drawing;
using FluentAssertions;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests;

public class ArchimedeanSpiral_Should
{
    [Test]
    public void GetNextPoint_WhenStartIsZero_WorksCorrectly()
    {
        ArchimedeanSpiral archSpiral = new(1, 1, 0);
        var actual = GetPoints(archSpiral, 6);

        var expected = new List<Point>();
        expected.Add(new Point(0, 0));
        expected.Add(new Point(1, 1));
        expected.Add(new Point(-1, 2));
        expected.Add(new Point(-3, 0));
        expected.Add(new Point(-3, -3));
        expected.Add(new Point(1, -5));

        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void GetNextPoint_WithNonZeroStart_WorksCorrectly()
    {
        ArchimedeanSpiral archSpiral = new(1, 3, 5);
        var actual = GetPoints(archSpiral, 6);

        var expected = new List<Point>();
        expected.Add(new Point(0, 0));
        expected.Add(new Point(4, 7));
        expected.Add(new Point(-5, 10));
        expected.Add(new Point(-14, 2));
        expected.Add(new Point(-11, -13));
        expected.Add(new Point(6, -19));

        actual.Should().BeEquivalentTo(expected);
    }

    [TestCase(0, 1, 0)]
    [TestCase(1, 0, 0)]
    [TestCase(1, 1, int.MaxValue)]
    [TestCase(int.MaxValue, 1, 1)]
    [TestCase(1, int.MaxValue, 1)]
    [TestCase(1_000_000, 1_000_000, 1)]
    [TestCase(1_000_000, 1_000, 2_000_000_000)]
    public void OnWrongParameters_ThrowsArgumentException(double step, double density, double start)
    {
        Action action = () => new ArchimedeanSpiral(step, density, start);
        action.Should().Throw<ArgumentException>();
    }

    [TestCase(1_000_000, 1_000, 1)]
    [TestCase(1000, 1000, 1_000_000_000)]
    public void GetNextPoint_OnNearMaxValues_WorksCorrectly(double step, double density, double start)
    {
        var archSpiral = new ArchimedeanSpiral(step, density, start);
        Action act = () => GetPoints(archSpiral, 10000);
        act.Should().NotThrow();
    }

    private List<Point> GetPoints(ArchimedeanSpiral archSpiral, int border)
    {
        var actual = new List<Point>();
        var count = 0;

        foreach (var point in archSpiral.GetNextPoint())
        {
            if (count == border)
                break;
            actual.Add(point);
            count++;
        }

        return actual;
    }
}