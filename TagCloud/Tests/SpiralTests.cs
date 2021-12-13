using System;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.PointGenerator;

namespace TagCloud.Tests
{
    [TestFixture]
    public class SpiralTests
    {
        [Test]
        public void GetCoordinates_FirstPointInCenter()
        {
            var center = new PointF(2, 6);
            var spiral = new Circle(0.2f, 0.5, center, new Cache());
            
            var firstPoint = spiral.GetPoints(new Size(1, 1)).First();
            
            firstPoint.Should().Be(center);
        }
        
        [Test]
        public void GetCoordinates_ReturnCoordinatesThatRadiusShouldIncrease()
        {
            var center = new PointF(9,2);
            var spiral = new Circle(0.2f, 0.5, center, new Cache());
            
            var points = spiral.GetPoints( new Size(5, 5)).Take(100).ToArray();
            
            for (var i = 1; i < points.Length; i++)
            {
                var previousRadius = center.DistanceTo(points[i - 1]);
                var currentRadius = center.DistanceTo(points[i]);
                Console.WriteLine(points[i]);
                (previousRadius < currentRadius).Should().BeTrue();
            }
        }
    }
}