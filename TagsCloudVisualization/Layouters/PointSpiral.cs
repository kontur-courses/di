using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TagsCloudVisualizationTest")]
namespace TagsCloudVisualization
{
    internal class PointSpiral : IInfinityPointsEnumerable
    {
        private readonly Point center;
        private readonly double dtheta;
        private readonly double densityParameter;

        public PointSpiral(Point center, uint densityParameter = 1, uint degreesDelta = 1)
        {
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