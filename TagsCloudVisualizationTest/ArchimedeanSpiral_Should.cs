using System;
using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;
using TagsCloudVisualization.CloudGenerating;
using FluentAssertions;

namespace TagsCloudVisualizationTest
{
    class ArchimedeanSpiral_Should
    {
        [Test]
        public void ReturnCenter_ForTheFirstCall_WhenCenterIsZeroPoint()
        {
            ReturnCenter_ForTheFirstCall(new PointF(0, 0));
        }

        [Test]
        public void ReturnCenter_ForTheFirstCall_WhenCenterNonZeroPoint()
        {
            ReturnCenter_ForTheFirstCall(new PointF(10, 20));
        }

        private void ReturnCenter_ForTheFirstCall(PointF center)
        {
            var spiral = new ArchimedeanSpiralGenerator(center, 10, 1);
            var firstPoint = spiral.GetNextPoint();
            AssertPointsAlmostEqual(center, firstPoint);
        }

        private void AssertPointsAlmostEqual(PointF expected, PointF actual)
        {
            const double eps = 0.01;
            Assert.AreEqual(expected.X, actual.X, eps);
            Assert.AreEqual(expected.Y, actual.Y, eps);
        }

        [Test]
        public void ReturnPointsWithDistanceEqualToStep_AfterFullTurn()
        {
            var step = 10f;
            var spiral = new ArchimedeanSpiralGenerator(new PointF(0, 0), step, 2 * (float) Math.PI);
            var first = spiral.GetNextPoint();
            var second = spiral.GetNextPoint();
            var distance = GetDistance(first, second);
            Assert.AreEqual(step, distance, 0.01);
        }

        private double GetDistance(PointF from, PointF to)
        {
            return Math.Sqrt(
                (from.X - to.X) * (from.X - to.X) +
                (from.Y - to.Y) * (from.Y - to.Y));
        }

        [Test]
        public void ReturnPointsInCounterclockwiseOrder()
        {
            var center = new Point(10, 10);
            var spiral = new ArchimedeanSpiralGenerator(center, 10, (float)Math.PI / 4);
            var stepsForFullRotation = 8;

            spiral.GetNextPoint(); // skip first point as it is exactly in the center

            var previous = spiral.GetNextPoint();
            for (var i = 0; i < stepsForFullRotation; i++)
            {
                var current = spiral.GetNextPoint();
                Assert.True(IsCounterclockwiseRotation(center, previous, current));
                previous = current;
            }
        }

        private bool IsCounterclockwiseRotation(PointF center, PointF from, PointF to)
        {
            return (from.X - center.X) * (to.Y - center.Y) -
                   (from.Y - center.Y) * (to.X - center.X) > 0;
        }

        [Test]
        public void ReturnPointsWithIncreasingDistanceToCenter()
        {
            var center = new PointF(0, 0);
            var spiral = new ArchimedeanSpiralGenerator(center, 10, (float) Math.PI / 4);

            var amountOfPoints = 10;
            var distances = new List<double>();
            for (var i = 0; i < amountOfPoints; i++)
            {
                var nextPoint = spiral.GetNextPoint();
                distances.Add(GetDistance(center, nextPoint));
            }

            distances.Should().BeInAscendingOrder();
        }
    }
}
