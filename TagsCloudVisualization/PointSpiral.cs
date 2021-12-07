using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class PointSpiral : IInfinityPointsEnumerable
    {
        private readonly Point center;
        private readonly double dtheta;
        private readonly double densityParameter;

        public PointSpiral(Point center, double densityParameter = 1, int degreesDelta = 1)
        {
            if (densityParameter <= 0f)
                throw new ArgumentException("densityParameter should be positive");
            if (degreesDelta <= 0)
                throw new ArgumentException("degreesDelta should be positive");
            
            this.center = center;
            this.densityParameter = densityParameter;
            
            dtheta = degreesDelta * Math.PI / 180;
        }

        public IEnumerable<Point> GetPoints()
        {
            var theta = 0d;
            
            while (true)
            {
                var radius = densityParameter * theta;
                yield return new Point(
                    (int)(radius * Math.Cos(theta)) + center.X,
                    (int)(radius * Math.Sin(theta)) + center.Y
                );
                theta += dtheta;
            }
            
            // ReSharper disable once IteratorNeverReturns
        }
    }
}