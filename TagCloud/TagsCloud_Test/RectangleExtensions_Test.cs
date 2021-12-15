using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Extensions;

namespace TagsCloud_Test
{
    public class RectangleExtensionsTest
    {
        [Test]
        public void GetDistancesToInnerPoint_ShouldBeCorrect()
        {
            var rectangle = new Rectangle(Point.Empty, new Size(100, 100));
            var point = new Point(50, 50);
            var expected = new List<int> {50, 50, 50, 50};
            rectangle.GetDistancesToInnerPoint(point).Should().Equal(expected);
        }

        [Test]
        public void GetDistancesToInnerPoint_ShouldThrow_WhenPointIsOutside()
        {
            var rectangle = new Rectangle(Point.Empty, new Size(100, 100));
            var point = new Point(-1, -1);
            var expected = new List<int> {50, 50, 50, 50};
            Action action = () => rectangle.GetDistancesToInnerPoint(point).Should().Equal(expected);
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetDistancesToInnerPoint_Distances_ShouldBePositive()
        {
            var rectangle = new Rectangle(Point.Empty, new Size(100, 100));
            var point = new Point(50, 50);
            rectangle.GetDistancesToInnerPoint(point).All(d => d >= 0).Should().BeTrue();
        }
    }
}