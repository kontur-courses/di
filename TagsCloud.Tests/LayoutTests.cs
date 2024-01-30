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
        var layoutFunction = new SpiralPointGenerator(random.Next(1, 25), random.NextSingle());
        var screenCenter = new PointF((float)WindowWidth / 2, (float)WindowHeight / 2);
        layout = new Layout(layoutFunction, screenCenter);

        // Clear currentRectangles between tests to update running context.
        currentRectangles.Clear();
        currentRectangles.TrimExcess();
    }

    private Layout layout;
    private readonly Random random = new();
    private readonly List<RectangleF> currentRectangles = new();

    [Test]
    public void PutNextRectangle_ShouldNot_SkipRectangles()
    {
        var rectCount = random.Next(1, 250);
        PutRectanglesInLayout(rectCount);

        layout.RectangleCount.Should().Be(rectCount);
    }

    [Test]
    public void PlacedRectangles_ShouldNot_HaveIntersections()
    {
        var rectCount = random.Next(1, 250);
        PutRectanglesInLayout(rectCount);

        CurrentRectanglesHaveIntersections().Should().Be(false);
    }

    private void PutRectanglesInLayout(int rectanglesCount)
    {
        for (var i = 0; i < rectanglesCount; i++)
        {
            var size = new SizeF(random.Next(1, 250), random.Next(1, 250));
            currentRectangles.Add(layout.PutNextRectangle(size));
        }
    }

    private bool CurrentRectanglesHaveIntersections()
    {
        return currentRectangles.SelectMany(
                                    curr => currentRectangles.Where(
                                        other => curr != other && curr.IntersectsWith(other)),
                                    (current, _) => current)
                                .Any();
    }
}