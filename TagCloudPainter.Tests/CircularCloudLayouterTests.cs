using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagCloudPainter.Extensions;
using TagCloudPainter.Layouters;

namespace TagCloudPainter.Tests;

public class CircularCloudLayouterTests
{
    private CircularCloudLayouter circularCloudLayouter;

    [SetUp]
    public void SetUp()
    {
        circularCloudLayouter = new CircularCloudLayouter(new Point(50, 50), Math.PI / 12, 0.25);
    }

    [TestCase(0, 1, TestName = "{m}_zero_width")]
    [TestCase(1, 0, TestName = "{m}_zero_height")]
    public void PutNextRectangle_Should_Fail_On(int width, int height)
    {
        var size = new Size(width, height);
        Action putNextRectangle = () => circularCloudLayouter.PutNextRectangle(size);

        putNextRectangle.Should().Throw<ArgumentException>();
    }

    [Test]
    public void PutNextRectangle_Should_Return_Two_Not_Intersects_Rectangles()
    {
        var firstRectangle = circularCloudLayouter.PutNextRectangle(new Size(10, 1));
        var secondRectangle = circularCloudLayouter.PutNextRectangle(new Size(10, 1));

        var result = firstRectangle.IntersectsWith(secondRectangle);

        result.Should().BeFalse();
    }

    [TestCase(5, 10, 5, ExpectedResult = false, TestName = "{m}_5_AddedRectangles")]
    [TestCase(500, 10, 5, ExpectedResult = false, TestName = "{m}_500_AddedRectangles")]
    public bool PutNextRectangle_Should_Return_Rectangle_Not_Intersects_Past(int n, int width, int height)
    {
        for (var i = 0; i < n; i++)
            circularCloudLayouter.PutNextRectangle(new Size(10, 5));
        var rectangles = circularCloudLayouter.Rectangles;
        var rectangle = circularCloudLayouter.PutNextRectangle(new Size(10, 2));

        var result = rectangle.IsIntersectsOthersRectangles(rectangles);

        return result;
    }

    [Test]
    public void PutNextRectangle_Should_Return_Rectangle_Not_Intersects_Past_Added_Rectangles_With_Different_Sizes()
    {
        for (var i = 1; i <= 10; i++)
            circularCloudLayouter.PutNextRectangle(new Size(i, i));
        var rectangles = circularCloudLayouter.Rectangles;
        var rectangle = circularCloudLayouter.PutNextRectangle(new Size(10, 2));

        var result = rectangle.IsIntersectsOthersRectangles(rectangles);
        result.Should().BeFalse();
    }

    [TearDown]
    public void AfterTests()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        {
            var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
            var path = Path.Combine(directoryInfo.Parent.Parent.Parent.FullName +
                                    @"\\FailedImages\\" + TestContext.CurrentContext.Test.Name + ".png");
            SaveFailedLayoutImage(path, new Size(100, 100), circularCloudLayouter.Rectangles);
            TestContext.WriteLine("Tag cloud visualization saved to file <{0}>", path);
        }
    }

    private void SaveFailedLayoutImage(string path, Size imageSize, IEnumerable<Rectangle> rectangles)
    {
        var btm = new Bitmap(imageSize.Width, imageSize.Height);
        var g = Graphics.FromImage(btm);

        foreach (var r in rectangles)
            g.DrawRectangle(new Pen(Color.Blue), r);

        btm.Save(path, ImageFormat.Png);
    }
}