using TagCloud.Layouter;
using TagCloud.PointGenerator;

[TestFixture]
public class CircularCloudLayouter_Should
{
    private const int Width = 1920;
    private const int Height = 1080;
    private CloudLayouter sut;
    
    [SetUp]
    public void Setup()
    {
        sut = new CloudLayouter(new SpiralGenerator(new Point(Width / 2, Height / 2)));
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