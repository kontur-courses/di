using System;
using NUnit.Framework;
using System.Drawing;
using FluentAssertions;
using TagsCloudVisualization.Layouter;

namespace TagsCloudVisualization_Tests
{
    [TestFixture]
    public class Spiral_Should
    {
        [TestCase(0, 0, 1, Math.PI / 2, 0, 1, TestName = "when non zero start angle")]
        [TestCase(0, 1, 1, Math.PI / 2, 0, 2, TestName = "when changing center point")]
        [TestCase(0, 0, 2, Math.PI / 2, 0, 3, TestName = "when changing spiral step")]
        [TestCase(0, 0, 1, Math.PI, -3, 0, TestName = "when changing angle")]
        public void GetNextPoint_GetsNextPointRight(int x, int y, double step, double angle, int xRes, int yRes)
        {
            var center = new Point(x, y);
            var spiral = new Spiral(center, step, angle);
            var point = spiral.GetNextPoint();
            point.Should().BeEquivalentTo(new Point(xRes, yRes));
        }
        }
}