using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudVisualization.CloudLayouter;
using TagsCloudVisualization.CloudLayouter.PointGenerator;
using TagsCloudVisualization.ColorGenerator;
using TagsCloudVisualization.Drawer;
using TagsCloudVisualization.ImageSavers;
using TagsCloudVisualization.ImageSettings;

namespace TagsCloudVisualization.Tests;

[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[Parallelizable(ParallelScope.All)]
public class CircularCloudLayouterTests
{
    private ICloudLayouter cloudLayouter;
    private List<Rectangle> rectangles;
    private Point center;

    [SetUp]
    public void Setup()
    {
        center = new Point(100, 100);
        cloudLayouter = new CircularCloudLayouter(new SpiralPointGenerator(center));
        rectangles = new List<Rectangle>();
    }

    [TearDown]
    public void TearDown()
    {
        if (TestContext.CurrentContext.Result.Outcome != ResultState.Failure)
            return;

        var directoryPath = Path.Join(Environment.CurrentDirectory, "Images");
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        var filename = $"{TestContext.CurrentContext.Test.Name}.png";
        var fullpath = Path.Combine(directoryPath, filename);
        var generator = new Drawer.ImageDrawer(new PngImageSaver(), new ImageSettingsProvider(Color.White, 1600, 1600));
        var colorGenerator = new RainbowColorGenerator(new Random());
        generator.Draw(rectangles.Select(r => new RectangleImage(r, colorGenerator)).ToList(), fullpath);
        TestContext.Error.WriteLine($"Tag cloud visualization saved to file {fullpath}");
    }

    [TestCase(1, -1, TestName = "{m}NegativeHeight")]
    [TestCase(-1, 1, TestName = "{m}NegativeWidth")]
    [TestCase(-11, -1, TestName = "{m}NegativeHeightAndWidth")]
    public void PutNextRectangle_ThrowArgumentException_On(int width, int height)
    {
        Action act = () => cloudLayouter.PutNextRectangle(new Size(width, height));

        act.Should().Throw<ArgumentException>();
    }

    [Test]
    public void PutNextRectangle_ReturnCentralRectangle_WhenPutFirstRectangle()
    {
        var size = new Size(100, 100);
        var expected = new Rectangle(center, size);

        var rectangle = cloudLayouter.PutNextRectangle(size);

        rectangle.Should().Be(expected);
    }

    [Test]
    public void PutNextRectangle_ReturnRectangle_WithDeclaredSize()
    {
        var size = GetRandomSize(0, int.MaxValue);
        var expected = new Rectangle(center, size);

        var rectangle = cloudLayouter.PutNextRectangle(size);

        rectangle.Should().Be(expected);
    }

    [TestCaseSource(nameof(RectanglesCountDataSource))]
    public void PutNextRectangle_RandomRectangleDoesNotIntersect_WithOthers(int count)
    {
        for (var i = 0; i < count; i++)
        {
            var rectangle = cloudLayouter.PutNextRectangle(GetRandomSize(40, 100));
            rectangles.Add(rectangle);
        }

        var intersectRectangles =
            rectangles.Where(current => rectangles.Any(rect => rect != current && rect.IntersectsWith(current)));

        intersectRectangles.Should().BeEmpty();
    }

    [TestCaseSource(nameof(RectanglesCountDataSource))]
    public void PutNextRectangle_ShouldCreateCloudLikeCircle(int count)
    {
        var greatestDistance = 0d;
        for (var i = 0; i < count; i++)
        {
            var rectangle = cloudLayouter.PutNextRectangle(new Size(50, 50));
            var distance = GetMaxDistanceToCorner(rectangle, center);
            if (distance > greatestDistance)
                greatestDistance = distance;
            rectangles.Add(rectangle);
        }

        var rectanglesArea = rectangles.Sum(r => r.Width * r.Height);
        var circleArea = Math.PI * greatestDistance * greatestDistance;
        var areaRatio = rectanglesArea / circleArea;

        areaRatio.Should().BeGreaterOrEqualTo(0.5);
    }

    private Size GetRandomSize(int minValue, int maxValue)
    {
        var random = new Random();
        return new Size(random.Next(minValue, maxValue), random.Next(minValue, maxValue));
    }

    private double GetMaxDistanceToCorner(Rectangle rectangle, Point from)
    {
        return GetCorners(rectangle).Select(to => DistanceBetween(from, to)).Max();
    }

    private IEnumerable<Point> GetCorners(Rectangle rectangle)
    {
        yield return new Point(rectangle.Left, rectangle.Top);
        yield return new Point(rectangle.Right, rectangle.Top);
        yield return new Point(rectangle.Right, rectangle.Bottom);
        yield return new Point(rectangle.Left, rectangle.Bottom);
    }

    private double DistanceBetween(Point from, Point to)
    {
        var x = from.X - to.X;
        var y = from.Y - to.Y;
        return Math.Sqrt(x * x + y * y);
    }

    private static TestCaseData[] RectanglesCountDataSource =
    {
        new TestCaseData(50),
        new TestCaseData(100),
        new TestCaseData(1000)
    };
}