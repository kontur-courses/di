using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Layout;

namespace TagsCloudContainer.Tests
{
    public class LinearScalerTests
    {
        [Test]
        public void GetValue_WithMinX_ShouldBeMinY()
        {
            new LinearScaler(new PointF(1, 1), new PointF(2, 10))
                .GetValue(1)
                .Should().Be(1);
        }

        [Test]
        public void GetValue_WithMaxX_ShouldBeMaxY()
        {
            new LinearScaler(new PointF(1, 1), new PointF(2, 10))
                .GetValue(2)
                .Should().Be(10);
        }

        [Test]
        public void GetValue_WithAverageX_ShouldBeAverageY()
        {
            new LinearScaler(new PointF(1, 1), new PointF(3, 10))
                .GetValue(2)
                .Should().Be(5.5f);
        }

        [Test]
        public void GetValue_WithEqualsX_ShouldBeAverageY()
        {
            new LinearScaler(new PointF(1, 1), new PointF(1, 10))
                .GetValue(1)
                .Should().Be(11 / 2f);
        }

        [Test]
        public void GetValue_WithNegativeFunction()
        {
            new LinearScaler(new PointF(1, 10), new PointF(3, 1))
                .GetValue(2)
                .Should().Be(5.5f);
        }
    }
}