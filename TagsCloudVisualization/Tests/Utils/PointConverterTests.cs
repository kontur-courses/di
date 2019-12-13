using System;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Utils;

namespace TagsCloudVisualization.Tests.Utils
{
    [TestFixture]
    public class PointConverterTests
    {
        public const double Epsilon = 1e-2;

        [TestCase(0, 0, 0, 0, TestName = "OnZeroR")]
        [TestCase(0, 15, 0, 0, TestName = "OnNonZeroThetaAndZeroR")]
        [TestCase(15, (float) Math.PI, -15, 0, TestName = "RandomCoordinates")]
        [TestCase(25, (float) Math.PI / 2, 0, 25, TestName = "RandomCoordinates2")]
        public void TransformPolarToCartesian_ReturnsCorrectValues(float r, float theta, float expectedX,
            float expectedY)
        {
            var (x, y) = PointConverter.TransformPolarToCartesian(r, theta);
            x.Should().BeApproximately(expectedX, (float) Epsilon);
            y.Should().BeApproximately(expectedY, (float) Epsilon);
        }

        [TestCase(0, 0, 0, 0, TestName = "ZeroIfInStart")]
        [TestCase(3, 4, 5, 0.92f, TestName = "RandomCoordinates")] //theta is Acos(3/5)
        [TestCase(-5, -12, 13, -1.96f, TestName = "RandomCoordinates2")]
        //theta is -Acos(-5/13)
        public void TransformCartesianToPolar_ReturnsCorrectValues(float x, float y, float expectedR,
            float expectedTheta)
        {
            var (r, theta) = PointConverter.TransformCartesianToPolar(x, y);
            r.Should().BeApproximately(expectedR, (float) Epsilon);
            theta.Should().BeApproximately(expectedTheta, (float) Epsilon);
        }
    }
}