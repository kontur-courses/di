using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class Spiral
    {
        private double currentSpiralAngle;
        private double angleShift = 0.05;

        private void IncreaseSpiralAngle()
        {
            currentSpiralAngle += angleShift;
        }

        public IEnumerable<Point> GenerateRectangleLocation()
        {
            //For generating rectangle location (left-upper corner)
            //I'm using Archimedean Spiral
            while (true)
            {
                var distanceBetweenTurnings = 1;
                var radius = distanceBetweenTurnings * currentSpiralAngle;

                var x = (int)(radius * Math.Cos(currentSpiralAngle));
                var y = (int)(radius * Math.Sin(currentSpiralAngle));         
                yield return new Point(x,y);
                IncreaseSpiralAngle();
            }
        }
    }
}