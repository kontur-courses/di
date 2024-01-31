using System.Diagnostics;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework.Interfaces;
using TagsCloudContainer.Image;
using TagsCloudContainer.TagCloud;
using TagsCloudContainer.UI;
using TagsCloudContainer.utility;

namespace TagsCloudContainerTests;

[TestFixture]
public class CircularCloudLayouterTests
{
    private CircularCloudLayouter circularCloudLayouter = null!;
    private const string FailOutputName = "failFile";
    private List<int> center = null!;

    [SetUp]
    public void CircularCloudLayouterSetUp()
    {
        var args = new ApplicationArguments();

        center = args.Center;

        circularCloudLayouter = new CircularCloudLayouter(args);
    }

    [TearDown]
    public void TagCloudVisualizerCircularCloudLayouterTearDown()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        {
            var args = new ApplicationArguments { Output = $"out/{FailOutputName}" };
            using var imageGenerator = new ImageGenerator(args);
            
            imageGenerator.DrawLayout(circularCloudLayouter.PlacedRectangles);
            Console.WriteLine("Tag cloud visualization saved to file " +
                              Utility.GetAbsoluteFilePath($"out/{FailOutputName}.jpg"));
        }
    }

    [Test]
    public void PutNextRectangle_OnEmptyLayout_Should_PlaceInCenter()
    {
        var size = new Size(10, 10);
        var actual = circularCloudLayouter.PutNextRectangle(size);
        var expected = new Rectangle(new Point(
            center[0] - size.Width / 2,
            center[1] - size.Height / 2
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

        var args = new ApplicationArguments();

        var tmpLayouter = new CircularCloudLayouter(args);

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

        var distToCenter = new Func<Point, List<int>, double>(
            (coord, cntr) => Math.Sqrt(
                (coord.X - cntr[0]) * (coord.X - cntr[0]) +
                (coord.Y - cntr[1]) * (coord.Y - cntr[1])
            ));

        var maxRadius = circularCloudLayouter.PlacedRectangles.Max(
            rectangle => Math.Max(
                Math.Max(
                    distToCenter(
                        rectangle.Location,
                        center),
                    distToCenter(
                        new Point(
                            rectangle.Location.X + rectangle.Width,
                            rectangle.Location.Y),
                        center)),
                Math.Max(
                    distToCenter(
                        new Point(
                            rectangle.Location.X,
                            rectangle.Location.Y + rectangle.Height),
                        center),
                    distToCenter(
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