using System;
using System.Drawing;
using Autofac;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Extensions;
using TagCloud.PointsSequence;
using TagCloud.Utility.Container;

namespace TagCloud.Tests.ForTagCloud
{
    [TestFixture]
    public class Spiral_Should
    {
        private readonly IContainer container = ContainerConfig.StandartContainer;

        [TestCase(0f, TestName = "Then step length is 0")]
        [TestCase(-0.00001f, TestName = "Then step length is small negative number")]
        [TestCase(-5f, TestName = "Then step length is negative")]
        public void SetStepLengthThrowArgumentException(float stepLength)
        {
            var spiral = container.Resolve<Spiral>();
            Action method = () =>
                spiral.SetStepLength(stepLength);

            method.Should().Throw<ArgumentException>();
        }

        [TestCase(0, 0)]
        [TestCase(-1, -1)]
        [TestCase(1, 1)]
        public void ReturnCenter_ThenGettingFirstPoint(int x, int y)
        {
            var center = new Point(x, y);
            var spiral = container.Resolve<Spiral>();
            spiral.SetCenter(center);

            var result = spiral.GetNextPoint();

            result.Should().Be(center);
        }

        [Test]
        public void ReturnPointsInAlmostCircleWay()
        {
            var spiral = container.Resolve<Spiral>();
            var points = new Point[100];

            for (var i = 0; i < points.Length; i++)
                points[i] = spiral.GetNextPoint();
            var size = points.GetBounds().Size;

            size.Width.Should().BeCloseTo(size.Height, 10);
        }

        [TestCase((float)Math.PI * 3f, TestName = "If step equal to PI * 3")]
        [TestCase(100f, TestName = "If step equal to 100")]
        public void ReturnDifferentPoints(float stepLength)
        {
            var spiral = container.Resolve<Spiral>();
            spiral.SetStepLength(stepLength);

            var first = spiral.GetNextPoint();
            var second = spiral.GetNextPoint();

            first.Should().NotBe(second);
        }

        [TestCase(0, TestName = "After 0 steps")]
        [TestCase(1, TestName = "After 1 steps")]
        [TestCase(1000, TestName = "After 1000 steps")]
        public void ResetEnumeration_ThenResetMethodCalled(int steps)
        {
            var spiral = container.Resolve<Spiral>();
            var first = spiral.GetNextPoint();

            for (var i = 0; i < steps; i++)
                spiral.GetNextPoint();
            spiral.Reset();

            first.Should().Be(spiral.GetNextPoint());
        }
    }
}