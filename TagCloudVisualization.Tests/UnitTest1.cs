namespace TagCloudVisualization.Tests;
using System;
using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using System.Linq;
using NUnit.Framework;
using TagsCloudVisualization;

[TestFixture]
public class CircularCloudLayouter_Should
{
    private CircularCloudLayouter cloud;
    private Point center;
    private List<Rectangle> rectangles;

    [SetUp]
    public void Setup()
    {
        center = new Point(250, 250);
        cloud = new CircularCloudLayouter(new SpiralPoints(center));
        rectangles = new List<Rectangle>();
    }
    
    [Test]
    public void PutNextRectangle_NoIntersects_AfterPutting()
    {
        var rectangleSize = new Size(10, 50);

        rectangles = new List<Rectangle>();
        for (var i = 0; i < 100; i++)
        {
            rectangles.Add(cloud.PutNextRectangle(rectangleSize));
        }

        foreach (var rectangle in rectangles)
        {
            foreach (var secondRectangle in rectangles.Where(x => x != rectangle))
            {
                rectangle.IntersectsWith(secondRectangle).Should().BeFalse();
            }
        }
    }

    [Test]
    public void PutNextRectangle_OnCenter_AfterFirstPut()
    {
        var rectangleSize = new Size(100, 50);
        var expectedRectangle = new Rectangle(center, rectangleSize);
        var rectangle = cloud.PutNextRectangle(rectangleSize);
        rectangles.Add(rectangle);
        rectangle.Should().BeEquivalentTo(expectedRectangle);
    }
}