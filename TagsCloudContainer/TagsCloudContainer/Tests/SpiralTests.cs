﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization.Tests
{
    [TestFixture()]
    public class SpiralTests
    {
        [TestCaseSource(nameof(cloudCenters))]
        public void GetPoints_ShouldGiveFirstPointCenter(Point center)
        {
            var spiral = new Spiral(center);
            var firstPoint = spiral.GetPoints().Take(1).First();
            firstPoint.Should().BeEquivalentTo(center);
        }
        
        private static IEnumerable<TestCaseData> cloudCenters = Enumerable 
            .Range(-1, 3) 
            .SelectMany(i => Enumerable 
                .Range(-1, 3) 
                .Select(j => new TestCaseData(new Point(i, j)).SetName("{m}: " + $"X = {i}, Y = {j}"))); 
    }
}