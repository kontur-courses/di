using System;
using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Core.Generators;

namespace TagsCloudContainer.Tests
{
    [TestFixture]
    class ArchimedeanSpiralTests
    {
        [TestCase(1, Math.PI / 2, Description = "Default parameters")]
        [TestCase(5, 5, Description = "Custom parameters")]
        public void GenerateNextPoint_ReturnsEmptyPoint_AtFirstGeneration(double distance, double delta)
        {
            var spiral = new ArchimedeanSpiral(distance, delta);

            spiral.GetNextPoint().Should().Be(PointF.Empty);
        }

        [Test]
        public void GenerateNextPoint_SavesDelta_DuringGeneration()
        {
            const double delta = Math.PI / 3;
            const double epsilon = Math.PI / 180;
            var spiral = new ArchimedeanSpiral(delta: delta);
            var points = new List<PointF>();

            for (var i = 0; i < 1000; i++)
            {
                points.Add(spiral.GetNextPoint());
            }

            for (var i = 1; i < points.Count; i++)
            {
                var previous = GetAngleToPoint(points[i - 1]);
                var current = GetAngleToPoint(points[i]);
                CalculateAngleDifference(current, previous)
                    .Should().BeInRange(delta - epsilon, delta + epsilon);
            }
        }

        [Test]
        public void GetNextPoint_SavesDistance_DuringGeneration()
        {
            const int distance = 10;
            var spiral = new ArchimedeanSpiral(distance, 2 * Math.PI);
            var points = new List<PointF>();

            for (var i = 0; i < 1000; i++)
            {
                points.Add(spiral.GetNextPoint());
            }

            for (var i = 1; i < points.Count; i++)
            {
                GetSquaredDistance(points[i - 1], points[i])
                    .Should().Be(distance * distance);
            }
        }

        private static double GetAngleToPoint(PointF point)
        {
            return Math.Atan2(point.Y, point.X);
        }

        private static double CalculateAngleDifference(double alpha, double beta)
        {
            var diff = Math.Abs(alpha - beta);
            return Math.Min(diff, 2 * Math.PI - diff);
        }

        private static double GetSquaredDistance(PointF first, PointF second)
        {
            return Math.Pow(first.X - second.X, 2) + Math.Pow(first.Y - second.Y, 2);
        }
    }
}