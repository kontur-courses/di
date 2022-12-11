using TagCloudContainer.PointAlgorithm;

namespace TagCloudShould.Infrastructure
{
    [TestFixture]
    public class ArithmeticSpiralShould
    {
        [Test, Timeout(500)]
        public void TimeoutArithmeticSpiral_WhenBigCountOperation()
        {
            var spiral = new ArithmeticSpiral();
            spiral.Config = new PointConfig(1, 1);
            for (var i = 0; i < 10000000; i++)
                spiral.GetNextPoint();
        }

        [Test]
        public void CreateArithmeticSpiral_WhenPointsIsPerpendicularlyEqual()
        {
            var spiral = new ArithmeticSpiral();
            spiral.Config = new PointConfig(1, 1);
            var pointList = new List<Point>();
            for (var i = 0; i < 100000; i++)
                pointList.Add(spiral.GetNextPoint());
            var yPoints = pointList.OrderBy(p => p.Y);
            var xPoints = pointList.OrderBy(p => p.X);
            Math.Abs(yPoints.First().Y).Should()
                .BeInRange(Math.Abs(yPoints.Last().Y) - 5, Math.Abs(yPoints.Last().Y) + 5);
            Math.Abs(xPoints.First().X).Should()
                .BeInRange(Math.Abs(xPoints.Last().X) - 5, Math.Abs(xPoints.Last().X) + 5);
        }
    }
}
