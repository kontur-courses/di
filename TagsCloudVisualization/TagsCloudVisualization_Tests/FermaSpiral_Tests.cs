using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagsCloudVisualization_Tests
{
    public class FermaSpiral_Tests
    {
        [TestCase(100, 5)]
        [TestCase(1000, 3)]
        public void FermaSpiral_ContainsAlmostEqual_CountOfPointsInQuarters(int countPoints, int accuracy)
        {
            var counts = GetPointCountsInQuarters(countPoints);

            IsElementsAlmostEqual(counts, accuracy).Should().BeTrue();
        }

        private static List<int> GetPointCountsInQuarters(int countPoints)
        {
            var points = new List<Point>();
            var spiral = new FermaSpiral(1, new Point(0, 0));

            for (var i = 0; i < countPoints; i++)
            {
                points.Add(spiral.GetSpiralNext());
            }

            var firstQuarterPointsCount = points.Count(p => p.X > 0 && p.Y > 0);
            var secondQuarterPointsCount = points.Count(p => p.X < 0 && p.Y > 0);
            var thirdQuarterPointsCount = points.Count(p => p.X > 0 && p.Y < 0);
            var fourthQuarterPointsCount = points.Count(p => p.X > 0 && p.Y > 0);
            var counts = new List<int>
            {
                firstQuarterPointsCount,
                secondQuarterPointsCount,
                thirdQuarterPointsCount,
                fourthQuarterPointsCount
            };
            return counts;
        }

        private static bool IsElementsAlmostEqual(List<int> counts, int accuracy)
        {
            foreach (var first in counts)
            {
                foreach (var second in counts)
                {
                    if (Math.Abs(first - second) > accuracy)
                        return false;
                }
            }

            return true;
        }
    }
}