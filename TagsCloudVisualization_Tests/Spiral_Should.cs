using System;
using NUnit.Framework;
using System.Drawing;
using TagsCloudVisualization;
using FluentAssertions;
using TagsCloudVisualization.Layouter;

namespace TagsCloudVisualization_Tests
{
    [TestFixture]
    public class Spiral_Should
    {
        [Test]
        public void GetNextPoint_GetsNextPointRight()
        {
            var center = new Point(0, 0);
            var spiral = new Spiral(center, 1, Math.PI / 2);
            var point = spiral.GetNextPoint();
            point.Should().BeEquivalentTo(new Point(0, 1));
        }
    }
}