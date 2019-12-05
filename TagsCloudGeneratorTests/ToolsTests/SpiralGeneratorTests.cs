using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudGenerator.CloudLayouter;

namespace TagsCloudGeneratorTests.ToolsTests
{
    public class SpiralGeneratorTests
    {
        [Test]
        public void GetSpiral_FirstPoint_ShouldReturnCenterCords()
        {
            var center = new Point(5, 12);
            var cords = SpiralGenerator.GetSpiral(center, 2);
            cords.First().Should().BeEquivalentTo(center);
        }

        [Test]
        public void GetSpiral_ShouldReturnRightPoints()
        {
            var center = new Point(0, 0);
            var expected = new List<Point>()
            {
                new Point(0, 0),
                new Point(0, 2),
                new Point(-5, 0),
                new Point(0, -7),
                new Point(10, 0)
            };

            var actual = SpiralGenerator.GetSpiral(center, 10, Math.PI / 2).Take(5).ToList();

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetSpiral_WithMovedCenter_ShouldReturnRightPoints()
        {
            var center = new Point(7, 15);
            var expected = new List<Point>()
            {
                new Point(7, 15),
                new Point(7, 17),
                new Point(2, 15),
                new Point(6, 7),
                new Point(17, 14),
                new Point(7, 27),
                new Point(-8, 15)
            };

            var actual = SpiralGenerator.GetSpiral(center, 10, Math.PI / 2).Take(7).ToList();

            actual.Should().BeEquivalentTo(expected);
        }
    }
}