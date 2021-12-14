using System.Drawing;
using NUnit.Framework;
using TagsCloudVisualization.Extensions;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class PointExtensionsTests
    {
        [TestCase(0, 0, 5, 5, ExpectedResult = 7.071)]
        [TestCase(-10, -12, -3, -4, ExpectedResult = 10.630)]
        [TestCase(0, 0, 0, 0, ExpectedResult = 0)]
        [DefaultFloatingPointTolerance(0.001)]
        public double GetDistance_PointsParams_ShouldReturnCorrectDistance(int x1, int y1, int x2, int y2)
        {
            return new Point(x1, y1).GetDistance(new Point(x2, y2));
        }
    }
}