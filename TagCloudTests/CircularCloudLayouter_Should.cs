using NUnit.Framework.Interfaces;

[TestFixture]
public class CircularCloudLayouter_Should
{
    private const int Width = 1920;
    private const int Height = 1080;
    private CircularCloudLayouter sut;


    [SetUp]
    public void Setup()
    {
        sut = new CircularCloudLayouter(new SpiralGenerator(new Point(Width / 2, Height / 2)));
    }

    [TearDown]
    public void TearDown()
    {
        if (TestContext.CurrentContext.Result.Outcome == ResultState.Failure)
        {
            var bitmap = CloudDrawer.DrawTagCloud(sut.Rectangles);

            var path = @$"{Environment.CurrentDirectory}\..\..\..\FailedTests\{this.GetType()}";
            var absPath = Path.GetFullPath(path);

            if (!Directory.Exists(absPath))
            {
                Directory.CreateDirectory(absPath);
            }

            var fileName = TestContext.CurrentContext.Test.Name;
            absPath += @$"\{fileName}.png";

            bitmap.Save(absPath);
            
            TestContext.Out.WriteLine($"Tag cloud visualization saved to file <{absPath}>");
        }
    }

    [Test]
    public void HaveEmptyRectanglesList_WhenCreated()
    {
        sut.Rectangles.Count.Should().Be(0);
    }

    [TestCase(1, TestName = "One rectangle")]
    [TestCase(10, TestName = "10 rectangles")]
    public void HaveAllRectangles_WhichWerePut(int rectanglesCount)
    {
        for (var i = 0; i < rectanglesCount; i++)
        {
            sut.PutNextRectangle(new Size(2, 3));
        }

        sut.Rectangles.Count.Should().Be(rectanglesCount);
    }

    [TestCase(0, 5, TestName = "Width is zero")]
    [TestCase(5, 0, TestName = "Height is zero")]
    [TestCase(-5, 5, TestName = "Width is negative")]
    [TestCase(5, -5, TestName = "Height is negative")]
    public void ThrowException_OnInvalidSizeArguments(int width, int height)
    {
        Action action = () => { sut.PutNextRectangle(new Size(0, 0)); };

        action.Should().Throw<ArgumentException>();
    }

    [Test]
    public void ContainNonIntersectingRectangles()
    {
        var random = new Random();

        for (var i = 0; i < 50; i++)
        {
            sut.PutNextRectangle(new Size(50 + random.Next(0, 100), 50 + random.Next(0, 100)));
        }

        HasIntersectedRectangles(sut.Rectangles).Should().Be(false);
    }

    private bool HasIntersectedRectangles(IList<Rectangle> rectangles)
    {
        for (var i = 0; i < rectangles.Count - 1; i++)
        {
            for (var j = i + 1; j < rectangles.Count; j++)
            {
                if (rectangles[i].IntersectsWith(rectangles[j]))
                    return true;
            }
        }

        return false;
    }
}