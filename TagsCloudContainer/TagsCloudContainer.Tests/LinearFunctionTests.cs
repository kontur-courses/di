using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.MathFunctions;

namespace TagsCloudContainer.Tests
{
    public class LinearFunctionTests
    {
        private LinearFunction function;

        [SetUp]
        public void SetUp()
        {
            function = new LinearFunction();
        }

        [Test]
        public void GetValue_WithMinX_ShouldBeMinY()
        {
            function.GetValue(new PointF(1, 1), new PointF(2, 10), 1)
                .Should().Be(1);
        }

        [Test]
        public void GetValue_WithMaxX_ShouldBeMaxY()
        {
            function.GetValue(new PointF(1, 1), new PointF(2, 10), 2)
                .Should().Be(10);
        }

        [Test]
        public void GetValue_WithAverageX_ShouldBeAverageY()
        {
            function.GetValue(new PointF(1, 1), new PointF(3, 10), 2)
                .Should().Be(5.5f);
        }

        [Test]
        public void GetValue_WithEqualsX_ShouldBeAverageY()
        {
            function.GetValue(new PointF(1, 1), new PointF(1, 10), 1)
                .Should().Be(11 / 2f);
        }

        [Test]
        public void GetValue_WithNegativeFunction()
        {
            function.GetValue(new PointF(1, 10), new PointF(3, 1), 2)
                .Should().Be(5.5f);
        }
    }
}