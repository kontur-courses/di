using System;
using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagCloudPainter.Layouters;

namespace TagCloudPainter.Tests;

public class HelixPointLayouterTests
{
    [TestCase(0, TestName = "{m}_ZeroRadiusStep")]
    [TestCase(-1, TestName = "{m}_NegativeRadiusStep")]
    public void Constructor_Should_Fail_On(double radiusStep)
    {
        Action action = () => new HelixPointLayouter(new Point(0, 0), Math.PI / 2, radiusStep);

        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void Constructor_Should_Fail_On_ZeroAngleStep()
    {
        Action action = () => new HelixPointLayouter(new Point(0, 0), 0, 1);

        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [TestCase(0, 0, TestName = "{m}_On_ZeroCenter")]
    [TestCase(1, 1, TestName = "{m}_On_NotZeroCenter")]
    public void GetPoint_Should_Return_CenterPoint_AfterFirstMethodCall(int x, int y)
    {
        var center = new Point(x, y);
        var helixPointLayouter = new HelixPointLayouter(center, 1, 1);
        var point = helixPointLayouter.GetPoint();

        point.Should().BeEquivalentTo(center);
    }

    [TestCase(0, 0, Math.PI / 12, 0.25, 5, TestName = "{m}_On_ZeroCenterPoint")]
    [TestCase(5, 5, Math.PI / 4, 0.2, 10, TestName = "{m}_On_NotZeroCenterPoint")]
    [TestCase(-1, -1, Math.PI / 6, 0.5, 100, TestName = "{m}_ON_NegativeCenterPoint")]
    public void GetPoint_Should_Return_CorrectPoints_WithDifferentStepParameters(int xCenter, int yCenter,
        double angleStep, double radiusStep, int n)
    {
        var center = new Point(xCenter, yCenter);
        var helixPointLayouter = new HelixPointLayouter(center, angleStep, radiusStep);
        var pointsList = new List<Point>();
        var helixPointList = new List<Point>();


        for (double i = 0, angle = 0, radius = 0; i < n; i++, angle += angleStep, radius += radiusStep)
        {
            var x = center.X + (int)(radius * Math.Cos(angle));
            var y = center.Y + (int)(radius * Math.Sin(angle));
            var point = new Point(x, y);
            pointsList.Add(point);
            helixPointList.Add(helixPointLayouter.GetPoint());
        }

        pointsList.Should().BeEquivalentTo(helixPointList);
    }
}