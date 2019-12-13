using System;
using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Layouters.Spirals;
using PointConverter = TagsCloudVisualization.Utils.PointConverter;

namespace TagsCloudVisualization.Tests.Layouters.Spirals
{
    [TestFixture]
    public class ArchimedesSpiralTests
    {
        [TearDown]
        public void Dispose()
        {
            generator?.Dispose();
        }

        private const double Epsilon = 1e-2;
        private const float DeltaAngle = (float) (5 * Math.PI / 180);
        private IEnumerator<PointF> generator;

        [TestCase(7, 0, 0, 0, 0, 0, TestName = "SpiralStartsAtCenter")]
        [TestCase(7, 5, 5, 0, 5, 5, TestName = "SpiralStartsAtCenterWhenCenterIsNotZero")]
        [TestCase(7, 0, 0, 5, 2.76f, 1.29f, TestName = "RandomPoint")]
        [TestCase(7, 5, 1, 5, 7.76f, 2.29f, TestName = "RandomPointWhenCenterIsNotZero")]
        [TestCase(3, 0, 0, 5, 1.18f, 0.55f, TestName = "RandomPointOnRandomThickness")]
        public void IEnumerator_YieldsCorrectValues(float thickness, float centerX, float centerY,
            int elementIndex, float expectedValueX, float expectedValueY)
        {
            generator = new ArchimedesSpiral(new PointF(centerX, centerY), thickness).GetSpiralPoints().GetEnumerator();
            for (var i = 0; i <= elementIndex; i++)
                generator.MoveNext();
            generator.Current.X.Should().BeApproximately(expectedValueX, (float) Epsilon);
            generator.Current.Y.Should().BeApproximately(expectedValueY, (float) Epsilon);
        }

        [TestCase(7, 0, 0, 20, TestName = "RandomPointOnSpiral")]
        [TestCase(2, 0, 0, 16, TestName = "RandomPointOnDifferentThickness")]
        [TestCase(2, 16, -2, 16, TestName = "RandomPointWhenCenterIsNotZero")]
        public void IEnumerator_YieldsSequenceInCorrectOrder(float thickness, float centerX, float centerY,
            int elementIndex)
        {
            generator = new ArchimedesSpiral(new PointF(centerX, centerY), thickness, DeltaAngle).GetSpiralPoints()
                .GetEnumerator();
            for (var i = 0; i <= elementIndex; i++)
                generator.MoveNext();
            var firstPoint = generator.Current;
            generator.MoveNext();
            float theta, r;
            (r, theta) = PointConverter.TransformCartesianToPolar(firstPoint.X - centerX, firstPoint.Y - centerY);
            theta += DeltaAngle;
            r = thickness * theta;
            var nextX = (float) (r * Math.Cos(theta)) + centerX;
            var nextY = (float) (r * Math.Sin(theta)) + centerY;
            generator.Current.X.Should().BeApproximately(nextX, (float) Epsilon);
            generator.Current.Y.Should().BeApproximately(nextY, (float) Epsilon);
        }
    }
}