using System.Diagnostics;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework.Interfaces;
using TagsCloudContainer.Image;
using TagsCloudContainer.TagCloud;
using TagsCloudContainer.utility;

namespace TagsCloudContainerTests;

public class CircularCloudLayouterTests
{
    private CircularCloudLayouter circularCloudLayouter = null!;
    private const string FailOutputName = "failFile";
    private Point center;

    [SetUp]
    public void CircularCloudLayouterSetUp()
    {
        center = new Point(960, 540);
        circularCloudLayouter = new CircularCloudLayouter(center);
    }

    [TearDown]
    public void TagCloudVisualizerCircularCloudLayouterTearDown()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        {
            using var imageGenerator = new ImageGenerator(
                Utility.GetRelativeFilePath($"out/{FailOutputName}"), ImageEncodings.Png,
                Utility.GetRelativeFilePath("src/JosefinSans-Regular.ttf"),
                30, 1920, 1080
            );
            imageGenerator.DrawLayout(circularCloudLayouter.PlacedRectangles);
            Console.WriteLine("Tag cloud visualization saved to file " +
                              Utility.GetRelativeFilePath($"out/{FailOutputName}.jpg"));
        }
    }

    [Test]
    public void PutNextRectangle_OnEmptyLayout_Should_PlaceInCenter()
    {
        var size = new Size(10, 10);
        var actual = circularCloudLayouter.PutNextRectangle(size);
        var expected = new Rectangle(new Point(
            center.X - size.Width / 2,
            center.Y - size.Height / 2
        ), size);
        actual.Should().Be(expected);
    }

    [Test]
    public void AlgorithmTimeComplexity_LessOrEqualQuadratic()
    {
        var words100Time = AlgorithmTimeComplexity(100);
        var words1000Time = AlgorithmTimeComplexity(1000);

        (words1000Time.TotalNanoseconds / words100Time.TotalNanoseconds).Should().BeLessOrEqualTo(100);
    }

    private TimeSpan AlgorithmTimeComplexity(int count)
    {
        var sw = new Stopwatch();

        sw.Start();

        var tmpLayouter = new CircularCloudLayouter(center);

        for (var _ = 0; _ < count; _++)
            tmpLayouter.PutNextRectangle(new Size(45, 15));

        sw.Stop();

        return sw.Elapsed;
    }

    [Test]
    public void Rectangles_NotIntersects()
    {
        for (var _ = 0; _ < 5; _++)
            circularCloudLayouter.PutNextRectangle(new Size(45, 15));

        circularCloudLayouter.PlacedRectangles
            .All(rect1 => circularCloudLayouter.PlacedRectangles
                .All(rect2 => rect1 == rect2 || !rect1.IntersectsWith(rect2))).Should().BeTrue();
    }

    [Test]
    public void TagCloudIsDensityAndShapeCloseToCircleWithCenter()
    {
        for (var _ = 0; _ < 100; _++)
            circularCloudLayouter.PutNextRectangle(new Size(45, 15));

        const double densityRatio = 0.3;

        var cloudArea = circularCloudLayouter.PlacedRectangles.Sum(rectangle => rectangle.Height * rectangle.Width);

        var maxRadius = circularCloudLayouter.PlacedRectangles.Max(
            rectangle => Math.Max(
                Math.Max(
                    PointMath.DistanceToCenter(
                        rectangle.Location,
                        center),
                    PointMath.DistanceToCenter(
                        new Point(
                            rectangle.Location.X + rectangle.Width,
                            rectangle.Location.Y),
                        center)),
                Math.Max(
                    PointMath.DistanceToCenter(
                        new Point(
                            rectangle.Location.X,
                            rectangle.Location.Y + rectangle.Height),
                        center),
                    PointMath.DistanceToCenter(
                        new Point(
                            rectangle.Location.X + rectangle.Width,
                            rectangle.Location.Y + rectangle.Height),
                        center))
            )
        );
        var outlineCircleArea = Math.PI * maxRadius * maxRadius;

        (cloudArea / outlineCircleArea).Should().BeGreaterThan(densityRatio);
    }
}