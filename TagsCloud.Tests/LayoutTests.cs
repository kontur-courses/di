using FluentAssertions;
using NUnit.Framework;
using SixLabors.ImageSharp;
using TagsCloudVisualization;
using static TagsCloud.Tests.TestConfiguration;

namespace TagsCloud.Tests;

[TestFixture]
public class LayoutTests
{
    [SetUp]
    public void SetUp()
    {
        var layoutFunction = new Spiral(random.Next(1, 25), random.NextSingle());
        var screenCenter = new PointF(WindowWidth / 2, WindowHeight / 2);
        layout = new Layout(layoutFunction, screenCenter);

        // Clear currentRectangles between tests to update running context.
        currentRectangles.Clear();
        currentRectangles.TrimExcess();
    }

    [TearDown]
    public void TearDown()
    {
        var context = TestContext.CurrentContext;
        var writer = TestContext.Out;

        if (context.Result.FailCount == 0)
            return;

        var fileName = $"{TestContext.CurrentContext.Test.MethodName}-fail.png";
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

        LayoutVisualizer.CreateVisualization(
            currentRectangles,
            ImageCenter,
            Color.White,
            1f,
            Color.Red,
            filePath);

        writer.WriteLine($"Tag cloud visualization saved to file <{filePath}>");
    }

    private Layout layout;
    private readonly Random random = new();
    private readonly List<RectangleF> currentRectangles = new();

    [Test]
    public void PutNextRectangle_ShouldNot_SkipRectangles()
    {
        var rectCount = random.Next(1, 250);
        PutNRectanglesInLayout(rectCount);

        layout.RectangleCount.Should().Be(rectCount);
    }

    [Test]
    public void PlacedRectangles_ShouldNot_HaveIntersections()
    {
        var rectCount = random.Next(1, 250);
        PutNRectanglesInLayout(rectCount);

        CurrentRectanglesHaveIntersections().Should().Be(false);
    }

    private void PutNRectanglesInLayout(int amount)
    {
        for (var i = 0; i < amount; i++)
        {
            var size = new SizeF(random.Next(1, 250), random.Next(1, 250));
            currentRectangles.Add(layout.PutNextRectangle(size));
        }
    }

    private bool CurrentRectanglesHaveIntersections()
    {
        return (from current in currentRectangles
            from another in currentRectangles
            where current != another
            where current.IntersectsWith(another)
            select current).Any();
    }
}