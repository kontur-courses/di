﻿using NUnit.Framework;
using System.Drawing;
using FluentAssertions;
using TagCloud.Extensions;


namespace TagsCloudVisualization_Test
{
    class PointExtensionsTest
    {
        [TestCase(50, 50, 0, 0)]
        [TestCase(0, 0, -50, -50)]
        public void GetRectangleLocationByCenter_ShouldGetCorrectLocation(int x, int y, int expectedX, int expectedY)
        {
            var size = new Size(100, 100);
            var actualLocation = new Point(x, y);
            var expected = new Point(expectedX, expectedY);
            actualLocation.GetRectangleLocationByCenter(size).Should().Be(expected);
        }
    }
}
