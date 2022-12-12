using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommandLine;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer;
using TagsCloudContainer.UI;

namespace TagCloudContainerTests
{
    public class SpiralTests
    {
        private ConsoleUi settings;

        [SetUp]
        public void SetUp()
        {
            settings = Parser.Default.ParseArguments<ConsoleUi>(new string[] { }).Value;
        }

        [Test]
        public void GetPoints_ShouldReturnFirstPointInCenter()
        {
            settings.AngleOffset = 1;
            settings.RadiusOffset = 1;
            var spiral = new Spiral(settings);
            var points = spiral.GetPoints().Take(5).ToList();
            points.First().Should().BeEquivalentTo(new Point(501, 501));
        }

        [TestCaseSource(nameof(_getPointsShouldReturnCorrectPointsCases))]
        public void GetPoints_ShouldReturnCorrectPoints(double radiusOffset, double angleOffset,
            List<Point> expectedPoints)
        {
            settings.AngleOffset = radiusOffset;
            settings.RadiusOffset = angleOffset;
            var spiral = new Spiral(settings);
            var points = spiral.GetPoints().Take(10).ToList();
            for (var i = 0; i < 10; i++)
                points[i].Should().BeEquivalentTo(expectedPoints[i]);
        }

        private static object[] _getPointsShouldReturnCorrectPointsCases =
        {
            new object[]
            {
                1, 1, new List<Point>
                {
                    new Point(501, 501),
                    new Point(499, 502),
                    new Point(497, 500),
                    new Point(497, 497),
                    new Point(501, 495),
                    new Point(506, 498),
                    new Point(505, 505),
                    new Point(499, 508),
                    new Point(492, 504),
                    new Point(492, 495)
                }
            },
            new object[]
            {
                -1, -1, new List<Point>
                {
                    new Point(499, 501),
                    new Point(501, 502),
                    new Point(503, 500),
                    new Point(503, 497),
                    new Point(499, 495),
                    new Point(494, 498),
                    new Point(495, 505),
                    new Point(501, 508),
                    new Point(508, 504),
                    new Point(508, 495)
                }
            }
        };
    }
}