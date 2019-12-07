using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Layouter;

namespace TagsCloudContainer.OldTests
{
    class PointExtensionsTests
    {
        [Test]
        public void SquaredDistanceTo_ShouldReturnSquaredDistanceBetweenPoints()
        {
            var firstPoint = new Point(1, 1);
            var secondPoint = new Point(4, 5);

            var result = firstPoint.SquaredDistanceTo(secondPoint);

            result.Should().Be(25);
        }

        [Test]
        public void DistanceTo_ShouldReturnDistanceBetweenPoints()
        {
            var firstPoint = new Point(1, 1);
            var secondPoint = new Point(4, 5);

            var result = firstPoint.DistanceTo(secondPoint);

            result.Should().Be(5);
        }
    }
}
