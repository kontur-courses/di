using System.Drawing.Imaging;
using NUnit.Framework.Interfaces;
using TagsCloudVisualization.CloudLayouters;
using TagsCloudVisualization.Extensions;
using TagsCloudVisualization.PointsProviders;
using TagsCloudVisualizationTests.Utils;


namespace TagsCloudVisualizationTests;

public class CircularCloudLayouterTests
{
    private Size imageSize;
    private Point center;
    private ITagsCloudLayouter layouter;
    
    [SetUp]
    public void SetUp()
    {
        imageSize = new Size(500, 500);
        center = new Point(imageSize.Width / 2, imageSize.Height / 2);
        layouter = new CircularCloudLayouter(new ArchimedeanSpiralPointsProvider(new ArchimedeanSpiralSettings {Center = center}));
    }

    [Test]
    public void Constructor_Success_OnPointArgument()
    {
        var a = () => new CircularCloudLayouter(new ArchimedeanSpiralPointsProvider(new ArchimedeanSpiralSettings {Center = center}));

        a.Should().NotThrow();
    }
    
    [Test]
    public void Constructor_ThrowsException_WhenPointsProviderIsNull()
    {
        var a = () => new CircularCloudLayouter(null);

        a.Should().Throw<ArgumentNullException>();
    }

    [TestCase(-2, 2, TestName = "rectangle size width is negative")]
    [TestCase(2, -2, TestName = "rectangle size height is negative")]
    [TestCase(-2, -2, TestName = "rectangle size width and height is negative")]
    [TestCase(0, 2, TestName = "rectangle size width is zero")]
    [TestCase(2, 0, TestName = "rectangle size height is zero")]
    [TestCase(0, 0, TestName = "rectangle size width and height is zero")]
    public void PutNextRectangle_ThrowsException_OnIncorrectArguments(int rectWidth, int rectHeight)
    {
        var rectSize = new Size(rectWidth, rectHeight);
        var a = () => layouter.PutNextRectangle(rectSize);
        
        a.Should().Throw<ArgumentException>();
    }
    
    [Test]
    public void PutNextRectangle_ReturnsRectangle_WithProvidedSize()
    {
        var rectSize = new Size(2, 2);

        layouter.PutNextRectangle(rectSize).Size.Should().Be(rectSize);
    }
    
    [Test]
    public void PutNextRectangle_ReturnsRectangleInCenter_OnFirstInvoke()
    {
        var rectSize = new Size(2, 2);
        var centerWithOffset = new Point(center.X - rectSize.Width / 2, center.Y - rectSize.Height / 2);

        layouter.PutNextRectangle(rectSize).Location.Should().Be(centerWithOffset);
    }

    [Test]
    public void PutNextRectangle_ReturnsUniqueRectangles()
    {
        var rectSize = new Size(2, 2);
        var iterations = 100;

        for (var i = 0; i < iterations; i++)
            layouter.PutNextRectangle(rectSize);
        
        layouter.Rectangles.Should().OnlyHaveUniqueItems();
    }
    
    [Test]
    public void PutNextRectangle_ReturnsRectangles_WithoutIntersections()
    {
        var rectSize = new Size(2, 2);
        var iterations = 100;

        for (var i = 0; i < iterations; i++)
            layouter.PutNextRectangle(rectSize);

        layouter.Rectangles
            .Should()
            .AllSatisfy(x => layouter.Rectangles.Count(y => y.IntersectsWith(x)).Should().Be(1));
    }

    [Test]
    [Repeat(3)]
    public void PutNextRectangle_CreatesTagCloud_WithHighDensity()
    {
        var random = new Random();
        var iterations = 1000;

        for (var i = 0; i < iterations; i++)
        {
            var rectSize = new Size(random.Next(70, 100), random.Next(70, 100));
            layouter.PutNextRectangle(rectSize);
        }

        var rectanglesSquare= layouter.Rectangles.Select(x => x.Height * x.Width).Sum();
        var circleRadius = layouter.Rectangles.GetDistanceToMostDistantPoint(layouter.Center);
        var circleSquare = Math.PI * circleRadius * circleRadius;
        var density = rectanglesSquare / circleSquare;

        density.Should().BeGreaterThan(0.8);
    }
    
    [TearDown]
    public void SaveImageWhenTestFails()
    {
        if (!TestContext.CurrentContext.Result.Outcome.Status.HasFlag(TestStatus.Failed)) return;
        var visualizator = new RectanglesCloudVisualizator(layouter);
        var pathToFolder = @"..\FailsTests";
        var fileName = TestContext.CurrentContext.Test.Name;
        visualizator.Draw().SaveAs(pathToFolder, fileName, ImageFormat.Png);
        
        TestContext.WriteLine($"Tag cloud visualization saved to file {Path.GetFullPath(Path.Combine(pathToFolder, fileName))}");
    }
}