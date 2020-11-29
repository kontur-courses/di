using FluentAssertions;
using NUnit.Framework;
using System;
using System.Drawing;
using TagsCloudVisualization;

namespace TagsCloudVisualization_Should
{
    public class PointProviderShould
    {
        [Test]
        public void GetPoint_ReturnPoint_AfterCallingMethod()
        {
            var center = new Point(500, 500);
            var pointProvider = new PointProvider(center);
            var expectedPoint = new Point(500, 500);

            var actualPoint = pointProvider.GetPoint();

            actualPoint.ShouldBeEquivalentTo(expectedPoint);
        }

        [Test]
        public void CreatePointProvider_ThrowArgumentException_CenterWithNegativeXOrY()
        {
            var point = new Point(-1, -1);

            Action act = () => new PointProvider(point);

            act.ShouldThrow<ArgumentException>().WithMessage("X or Y of center was negative");
        }
    }
}
