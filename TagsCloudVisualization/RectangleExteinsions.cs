using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization
{
    public static class RectangleExtensions
    {
        public static bool IntersectWithAny(this Rectangle candidate, IEnumerable<Rectangle> rectangleList)
        {
            return rectangleList.Any(rectangle => rectangle.IntersectsWith(candidate));
        }


        public static double DistanceTo(this Rectangle rectangle, Vector cloudCenter)
        {
            var distVector = rectangle.TopLeft() + (Vector) rectangle.Size / 2 - cloudCenter;
            return distVector.Length();
        }

        public static Vector TopLeft(this Rectangle rectangle)
        {
            return new Vector(rectangle.X, rectangle.Y);
        }
        public static Vector Center(this Rectangle rectangle)
        {
            return rectangle.TopLeft()+(Vector)rectangle.Size/2;
        }
        
    }

   
}