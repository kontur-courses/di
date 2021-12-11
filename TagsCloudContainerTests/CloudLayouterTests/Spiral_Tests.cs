using System;
using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.CloudLayouter;
using TagsCloudContainer.Settings;

namespace TagsCloudContainerTests.CloudLayouterTests
{
    public class ArchimedeanSpiral_Tests
    {
        private ArchimedeanSpiral spiral;
        private List<Point> points;

        [SetUp]
        public void Setup()
        {
            spiral = new ArchimedeanSpiral(new AppSettings(){AngleStep = Math.PI / 45, Density = 0.5});
            points = new List<Point>();
        }


        [Test]
        public void GetNextPoint_ReturnsSpiralCenter_OnFirstCall()
        {
            var point = spiral.GetNextPoint();
            point.Should().Be(Point.Empty);
        }

        [Test]
        public void GetNextPoint_EveryNextPointShouldBeUnique()
        {
            for (var i = 0; i < 250; i++)
            {
                points.Add(spiral.GetNextPoint());
            }

            points.Should().OnlyHaveUniqueItems();
        }
    }
}