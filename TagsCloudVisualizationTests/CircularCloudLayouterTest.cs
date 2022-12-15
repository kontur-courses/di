using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudVisualization;
using TagsCloudVisualization.CloudLayouter;

namespace TagsCloudVisualizationTests;

[TestFixture]
public class CircularCloudLayouterTest
{
    private CircularCloudLayouter _layouter;
    private PointF _center;

    private LayoutOptions _options;

    [SetUp]
    public void Setup()
    {
        var spiral = new ArithmeticSpiral();
        _center = new Point(0, 0);
        _layouter = new CircularCloudLayouter(spiral);
        _options = new LayoutOptions(_center, 0.1f);
    }

    [Test]
    public void Constructor_NotPositiveSpiralStep_Throw()
    {
        new Action(() => { new CircularCloudLayouter(new ArithmeticSpiral(-1)); })
            .Should()
            .Throw<ArgumentException>();
    }

    [TestCase(0, 0, TestName = "Zero size")]
    [TestCase(5, 0, TestName = "Zero height")]
    [TestCase(0, 5, TestName = "Zero width")]
    [TestCase(-1, 5, TestName = "Negative width")]
    [TestCase(5, -1, TestName = "Negative height")]
    public void PutNextRectangle_NotPositiveOrSingleSideSize_EmptyRectangle(int width, int height)
    {
        var rectangle = _layouter.PutNextRectangle(new Size(0, 0), _options);
        rectangle.IsEmpty.Should().BeTrue();
    }

    [Test]
    public void PutNextRectangle_ZeroSize_EmptyRectangle()
    {
        var rectangle = _layouter.PutNextRectangle(new Size(0, 0), _options);
        rectangle.IsEmpty.Should().BeTrue();
    }

    [Test]
    public void PutNextRectangle_RightSize_RectangleSizeEqual()
    {
        var random = new Random();
        var width = random.Next(1, 100);
        var height = random.Next(1, 100);

        var rectangle = _layouter.PutNextRectangle(new Size(width, height), _options);

        using (new AssertionScope())
        {
            rectangle.Width.Should().Be(width);
            rectangle.Height.Should().Be(height);
        }
    }


    [Test]
    public void PutNextRectangle_FirstRectangle_ShiftToCenterOfRectangle()
    {
        var x = 4;
        var y = 6;
        var width = 9;
        var height = 12;

        _options = new LayoutOptions(new PointF(x, y), 0.1f);
        var rectangle = _layouter.PutNextRectangle(new Size(width, height), _options);

        var expectedX = x - (float)width / 2;
        var expectedY = y - (float)height / 2;

        using (new AssertionScope())
        {
            rectangle.X.Should().Be(expectedX);
            rectangle.Y.Should().Be(expectedY);
        }
    }


    [TestCase(4, 4)]
    [TestCase(7, 3)]
    [TestCase(5, 7)]
    public void PutNextRectangle_TwoRectangles_NotIntersect(int width, int height)
    {
        var firstRectangle = _layouter.PutNextRectangle(new Size(width, height), _options);
        var secondRectangle = _layouter.PutNextRectangle(new Size(width, height), _options);

        firstRectangle
            .IntersectsWith(secondRectangle)
            .Should()
            .BeFalse();
    }

    [Test]
    public void PutNextRectangle_ManyRectangles_AllNotIntersect()
    {
        var random = new Random();
        var rectangles = new List<RectangleF>();


        for (int i = 0; i < 100; i++)
        {
            var newX = random.Next(40, 100);
            var newY = random.Next(40, 100);
            rectangles.Add(_layouter.PutNextRectangle(new Size(newX, newY), _options));
        }

        foreach (var rectangle in rectangles)
        {
            rectangles.Where(rect => rect != rectangle)
                .Should().AllSatisfy(x => rectangle.IntersectsWith(x).Should().BeFalse());
        }
    }


    [Test]
    public void PutNextRectangle_ManyRectangles_AllPutInLayout()
    {
        var random = new Random();
        var rectangles = new List<RectangleF>();

        for (int i = 0; i < 100; i++)
        {
            var newX = random.Next(40, 100);
            var newY = random.Next(40, 100);
            rectangles.Add(_layouter.PutNextRectangle(new Size(newX, newY), _options));
        }

        _layouter.Rectangles.Should().BeEquivalentTo(rectangles);
    }


    [TearDown]
    public void TearDown()
    {

    }
}