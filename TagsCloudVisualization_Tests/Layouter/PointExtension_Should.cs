using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Layouter;

namespace TagsCloudVisualization_Tests
{
    [TestFixture]
    public class PointExtension_Should
    {
        [TestCase(0, 0, 0, 1, ExpectedResult = 1, TestName = "with only one non-zero coordinate result is its value")]
        [TestCase(0, 0, 1, 1, ExpectedResult = 1, TestName = "with no one non-zero coordinate")]
        [TestCase(0, 0, 0, 0, ExpectedResult = 0, TestName = "with no non-zero result is zero")]
        public int GetDistanceTo_ReturnCorrectDistance(int x1, int y1, int x2, int y2) => new Point(x1, y1).GetDistanceTo(new Point(x2, y2));

        [Test]
        public void Add_ReturnCorrectPoint()
        {
            new Point(1, -1).Add(new Point(-1, 1)).Should().Be(new Point(0, 0));
        }
    }


}