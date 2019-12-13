using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Infrastructure;

namespace TagCloudTests.Infrastructure
{
    public class SizeUtilsTests
    {
        [TestCase(100, 100, 2, 1, 10, TestName = "Very big main size")]
        [TestCase(6, 3, 2, 1, 9, TestName = "Exact main size")]
        [TestCase(7, 5, 3, 2, 4, TestName = "Almost exact main size")]
        public void CanFillRectangleWithRectangles_ShouldBeTrue_WhenCanFill(
            int mainWidth, int mainHeight, int innerWidth, int innerHeight, int count)
        {
            var mainSize = new Size(mainWidth, mainHeight);
            var innerSize = new Size(innerWidth, innerHeight);

            var result = SizeUtils.CanFillSizeWithSizes(mainSize, innerSize, count);

            result.Should().BeTrue();
        }

        [TestCase(2, 1, 100, 100, 10, TestName = "Very big inner size")]
        [TestCase(7, 3, 2, 1, 10, TestName = "One extra rectangle")]
        public void CanFillRectangleWithRectangles_ShouldBeFalse_WhenCanNotFill(
            int mainWidth, int mainHeight, int innerWidth, int innerHeight, int count)
        {
            var mainSize = new Size(mainWidth, mainHeight);
            var innerSize = new Size(innerWidth, innerHeight);

            var result = SizeUtils.CanFillSizeWithSizes(mainSize, innerSize, count);

            result.Should().BeFalse();
        }
    }
}
