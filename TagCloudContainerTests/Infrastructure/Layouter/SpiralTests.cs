using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Infrastructure.Layouter;

namespace TagCloudTests.Infrastructure.Layouter;

public class SpiralTests
{
    private Spiral sut;

    [SetUp]
    public void SetUp()
    {
        sut = new Spiral(new Point(0, 0));
    }

    [Test]
    public void Constructor_ThrowArgumentException_WhenSpiralParamLessOrEqualZero()
    {
        Action action = () => new Spiral(new Point(), -1);

        action.Should().Throw<ArgumentException>().WithMessage("Spiral param must be greater than zero");
    }

    [Test]
    public void GetNextPoint_ReturnsCenter_WhenFirstCall()
    {
        var center = sut.Center;

        var point = sut.GetNextPoint();

        point.Should().BeEquivalentTo(center);
    }

    [Test]
    public void GetNextPoint_ShouldIncreaseDistance_WhenManyCalls()
    {
        const int pointsCount = 1000;
        var point = sut.GetNextPoint();

        for (var i = 1; i < pointsCount / 2; i++)
            point = sut.GetNextPoint();
        var halfDistance = point.GetDistance(sut.Center);
        for (var i = pointsCount / 2; i < pointsCount; i++)
            point = sut.GetNextPoint();
        var fullDistance = point.GetDistance(sut.Center);

        fullDistance.Should().BeGreaterThan(halfDistance);
    }

    [Test]
    public void GetNextPoint_ShouldGenerateUniquePoints_WhenManyCalls()
    {
        const int pointsCount = 1000;
        var points = new List<Point>();

        for (var i = 0; i < pointsCount; i++)
            points.Add(sut.GetNextPoint());

        var hasDuplicates = points.GroupBy(x => x).Any(x => x.Count() > 1);
        hasDuplicates.Should().BeFalse();
    }
}