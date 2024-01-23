using TagCloud.PointGenerator;

[TestFixture]
public class SpiralGenerator_Should
{
    private const int Width = 1920;
    private const int Height = 1080;
    private SpiralGenerator sut;
    private Point startPoint;

    [SetUp]
    public void Setup()
    {
        startPoint = new Point(Width / 2, Height / 2);
        sut = new SpiralGenerator(startPoint, 1, 0.1);
    }

    [Test]
    public void ReturnCenterPoint_OnFirstCall()
    {
        sut.GetNextPoint().Should().BeEquivalentTo(startPoint);
    }

    [Test]
    public void ReturnDifferentPoints_AfterMultipleCalls()
    {
        var spiralPoints = new HashSet<Point>();

        for (var i = 0; i < 50; i++)
        {
            spiralPoints.Add(sut.GetNextPoint());
        }

        spiralPoints.Count.Should().BeGreaterThan(1);
    }

    [TestCase(-1, 1, TestName = "X is negative")]
    [TestCase(1, -1, TestName = "Y is negative")]
    [TestCase(-1, -1, TestName = "X and Y are negative")]
    public void ThrowException_OnInvalidCenterPoint(int x, int y)
    {
        Action action = () => { new SpiralGenerator(new Point(x, y)); };

        action.Should().Throw<ArgumentException>();
    }
}