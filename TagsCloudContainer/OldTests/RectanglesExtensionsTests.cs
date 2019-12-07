using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Layouter;

namespace TagsCloudContainer.OldTests
{
    class RectanglesExtensionsTests
    {
        [Test]
        public void GetCorners_ShouldReturnCorrectCorners()
        {
            var rectangle = new Rectangle(-10, -5, 20, 10);

            var corners = rectangle.GetCorners();
            var leftTop = corners[0];
            var rightTop = corners[1];
            var rightBottom = corners[2];
            var leftBottom = corners[3];

            leftTop.Should().Be(new Point(-10, -5));
            rightTop.Should().Be(new Point(10, -5));
            rightBottom.Should().Be(new Point(10, 5));
            leftBottom.Should().Be(new Point(-10, 5));
        }
    }
}
