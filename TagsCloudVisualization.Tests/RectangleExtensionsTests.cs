using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Extensions;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class RectangleExtensionsTests
    {
        [TestCase(50, 60, 25, 15)]
        [TestCase(0, 0, 10, 10)]
        [TestCase(-50, -60, 25, 15)]
        public void GetRectangleCenter_RectParams_ShouldCalcRectCenterCorrectly(int x, int y, int width, int height)
        {
            var expectedCenter = new Point(x + width / 2, y + height / 2);
            var actualCenter = new Rectangle(x, y, width, height).GetCenter();
            actualCenter.Should().BeEquivalentTo(expectedCenter);
        }
    }
}