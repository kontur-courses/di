using System;
using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.CloudLayouter.PointGenerator;

namespace TagsCloudVisualization.Tests;

public class SpiralPointGeneratorTests
{
    [TestCase(0, 0.2f, TestName = "{m}ZeroParameter")]
    [TestCase(2, 0f, TestName = "{m}ZeroStep")]
    public void Constructor_ThrowArgumentException_On(int parameter, float step)
    {
        Action act = () => new SpiralPointGenerator(Point.Empty, parameter, step);

        act.Should().Throw<ArgumentException>();
    }

    [Test]
    public void Constructor_DoesntThrowArgumentException_OnCorrectArguments()
    {
        Action act = () => new SpiralPointGenerator(Point.Empty, 1, 0.2f);

        act.Should().NotThrow<ArgumentException>();
    }

    [TestCaseSource(nameof(SpiralPointGeneratorDataSource))]
    public IEnumerable<Point> Next_ShouldReturnCorrectNextPoint_With(Point center, int distance, double angleStep,
        int count)
    {
        var spiral = new SpiralPointGenerator(center, distance, angleStep);

        var actual = new List<Point>();
        for (var i = 0; i < count; i++)
        {
            actual.Add(spiral.Next());
        }

        return actual;
    }

    private static TestCaseData[] SpiralPointGeneratorDataSource =
    {
        new TestCaseData(new Point(100, 100), 2, 0.4, 4)
            .SetName("{m}PositiveArguments")
            .Returns(new Point[]
            {
                new Point(100, 100), new Point(101, 100), new Point(101, 101), new Point(101, 102)
            }),

        new TestCaseData(new Point(100, 100), -2, 0.4, 4)
            .SetName("{m}NegativeParameter")
            .Returns(new Point[]
            {
                new Point(100, 100), new Point(99, 100), new Point(99, 99), new Point(99, 98)
            }),

        new TestCaseData(new Point(100, 100), 2, -0.4, 4)
            .SetName("{m}NegativeStep")
            .Returns(new Point[]
            {
                new Point(100, 100), new Point(99, 100), new Point(99, 101), new Point(99, 102)
            }),

        new TestCaseData(new Point(100, 100), -2, -0.4, 4)
            .SetName("{m}NegativeParameters")
            .Returns(new Point[]
            {
                new Point(100, 100), new Point(101, 100), new Point(101, 99), new Point(101, 98)
            }),
    };
}