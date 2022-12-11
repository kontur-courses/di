using FluentAssertions;
using NUnit.Framework;
using System.Drawing;
using TagsCloud;

namespace TagsCloudTests
{
    [TestFixture]
    public class SpiralShould
    {
        [TestCase(0, 0)]
        [TestCase(1000, 500)]
        [TestCase(-200, -30)]
        public void Spiral_Create_ShouldHaveRightCenter(int centerX, int centerY)
        {
            var center = new Point(centerX, centerY);

            var spiral = new Spiral(center);

            spiral.Center.Should().Be(center);
        }

        [Test]
        public void Spiral_CreateWithParams_ShouldHave2Points()
        {
            var center = Point.Empty;

            var spiral = new Spiral(center, 30, 10);

            spiral.Points.Count.Should().Be(2);
        }

        [Test]
        public void Spiral_CreateWithoutParams_ShouldHave2Points()
        {
            var center = Point.Empty;

            var spiral = new Spiral(center);

            spiral.Points.Count.Should().Be(2);
        }

        [TestCase(-5)]
        [TestCase(0)]
        [TestCase(5)]
        [TestCase(100)]
        public void AddMorePointsInSpiral_AddPoints_ShouldAddNewPoints(int quantity)
        {
            var center = Point.Empty;
            var spiral = new Spiral(center);
            int expectedQuantity;

            if (quantity <= 0)
            {
                expectedQuantity = spiral.Points.Count;
            }
            else
            {
                expectedQuantity = spiral.Points.Count + quantity;
            }

            var spiralPoints = spiral.GetSpiralPoints();
            var enumerator = spiralPoints.GetEnumerator();

            for (int i = 0; i < quantity + 2; i++)
            {
                enumerator.MoveNext();
            }

            spiral.Points.Count.Should().Be(expectedQuantity);
            spiral.FreePoints.Count.Should().Be(expectedQuantity);
        }
    }
}
