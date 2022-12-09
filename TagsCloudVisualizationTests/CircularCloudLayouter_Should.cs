using System.Drawing;
using FluentAssertions;
using NUnit.Framework.Interfaces;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests;

public class CircularCloudLayouter_Should
{
    private CircularCloudLayouter _circularCloudLayouter;
    private List<Rectangle> _rectangles;

    [SetUp]
    public void SetUp()
    {
        var center = new Point(1500, 1500);
        _rectangles = new List<Rectangle>();
        ICurve archSpiral = new ArchimedeanSpiral(10, 10, 0);
        _circularCloudLayouter = new CircularCloudLayouter(center, archSpiral);
    }

    [TearDown]
    public void WhenTestFailed_DrawActual()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed)
            return;

        var maxAttemptsCount = 250;
        for (var i = 0; i < maxAttemptsCount; i++)
        {
            var testName = TestContext.CurrentContext.Test.Name;
            var path = $"FailedTests\\{testName}";
            var filename = $"{path}\\Attempt{i}.png";
            Directory.CreateDirectory(path);

            if (!File.Exists(filename))
            {
                var pen = new Pen(Color.Black, 2);
                var layoutDrawer = new LayoutDrawer(pen);
                layoutDrawer.Draw(_rectangles, filename);
                break;
            }
        }
    }

    [Test]
    public void WhenNoPlaceForRectangle_ThrowsException()
    {
        _circularCloudLayouter.PutNextRectangle(new Size(int.MaxValue, int.MaxValue));
        Action act = () => _circularCloudLayouter.PutNextRectangle(new Size(100, 100));
        act.Should().Throw<ArgumentException>();
    }

    [Test]
    public void OnOneRectangle_ReturnsRectangleWithHisCenterAsLocation()
    {
        var size = new Size(50, 100);
        var actual = _circularCloudLayouter.PutNextRectangle(size);
        actual.Should().Be(new Rectangle(new Point(1475, 1450), size));
        _rectangles.Add(actual);
    }

    [TestCase(-10, 20)]
    [TestCase(20, -10)]
    [TestCase(0, 0)]
    [TestCase(-10, -20)]
    public void OnNonPositiveWidthAndHeight_ThrowsArgumentException(int width, int height)
    {
        var size = new Size(width, height);
        Action act = () => _circularCloudLayouter.PutNextRectangle(size);
        act.Should().Throw<ArgumentException>().WithMessage("Width and Height must be positive!");
    }

    [Test]
    public void LayoutHasNoIntersectingRectangles()
    {
        var minSize = new Size(25, 25);
        var maxSize = new Size(50, 50);
        var cloudGenerator = new CloudGenerator(150, minSize, maxSize, _circularCloudLayouter);

        var actual = cloudGenerator.GetGeneratedCloud();
        var hasIntersection = false;
        foreach (var curRectangle in actual)
        foreach (var rectangleToCompare in actual)
        {
            if (rectangleToCompare == curRectangle)
                continue;

            if (curRectangle.IntersectsWith(rectangleToCompare))
            {
                hasIntersection = true;
                break;
            }
        }

        hasIntersection.Should().BeFalse();
    }

    [Test]
    public void OnFewRectangleSizes_ReturnsRectanglesWithRightLocation()
    {
        _rectangles = new List<Rectangle>();
        Size s1, s2, s3, s4;
        s1 = new Size(20, 30);
        s2 = new Size(20, 30);
        s3 = new Size(50, 20);
        s4 = new Size(10, 20);
        _rectangles.Add(_circularCloudLayouter.PutNextRectangle(s1));
        _rectangles.Add(_circularCloudLayouter.PutNextRectangle(s2));
        _rectangles.Add(_circularCloudLayouter.PutNextRectangle(s3));
        _rectangles.Add(_circularCloudLayouter.PutNextRectangle(s4));

        var expected = new List<Rectangle>();
        expected.Add(new Rectangle(new Point(1490, 1485), s1));
        expected.Add(new Rectangle(new Point(1406, 1431), s2));
        expected.Add(new Rectangle(new Point(1557, 1673), s3));
        expected.Add(new Rectangle(new Point(1541, 1194), s4));

        _rectangles.Should().BeEquivalentTo(expected);
    }
}