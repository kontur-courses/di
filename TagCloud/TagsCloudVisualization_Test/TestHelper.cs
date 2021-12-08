using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Extensions;

namespace TagsCloudVisualization_Test
{
    public static class TestHelper
    {
        public static List<Size> GenerateSizes(int tagsCount)
        {
            var sises = new List<Size>();
            var rnd = new Random();
            for (int i = 0; i < tagsCount; i++)
            {
                var width = rnd.Next(14, 60);
                var height = rnd.Next(10, width);
                sises.Add(new Size(width, height));
            }
            return sises;
        }

        public static double GetDensityFactor(List<Rectangle> rectangles, Point center)
        {
            var union = rectangles.First();
            double squareSum = 0;
            foreach (var r in rectangles)
            {
                union = Rectangle.Union(union, r);
                squareSum += r.Width * r.Height;
            }
            var radius = union.GetDistancesToInnerPoint(center).Average();
            var sphereSquare = Math.PI * radius * radius;
            return squareSum / sphereSquare;
        }

        public static List<Rectangle> CheckIntersects(List<Rectangle> rectangles)
        {
            return rectangles
                .SelectMany(rect => rectangles
                .Where(other => other != rect)
                .Where(rect.IntersectsWith)
                .Select(other => rect.GetIntersection(other)))
                .Distinct()
                .ToList();
        }

        public static Rectangle UnionAll(List<Rectangle> rectangles)
        {
            var union = rectangles.First();
            foreach (var r in rectangles)
                union = Rectangle.Union(union, r);
            return union;
        }

        public static List<Size> GenerateSizes_WithOneVeryBig(int tagsCount)
        {
            var sizes = new List<Size>();
            sizes.AddRange(GenerateSizes(5));
            sizes.Add(new Size(2000, 1000));
            sizes.AddRange(GenerateSizes(tagsCount));
            return sizes;
        }
    }
}
