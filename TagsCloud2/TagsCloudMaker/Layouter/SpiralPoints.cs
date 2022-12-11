using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class SpiralPoints : IEnumerator<Point>
    {
        private Point spiralCenter;
        private RadiusVector lastRadiusVector;
        private int distanceBetweenPoints = 25;
        private int spiralStep = 500;
        private int startAngle;
        private int fullAngle = 360;

        public SpiralPoints(Point spiralCenter, int startAngle)
        {
            this.spiralCenter = spiralCenter;
            this.startAngle = startAngle;
        }
        
        private void GetNextRadiusVector()
        {
            if (lastRadiusVector == null)
            {
                lastRadiusVector = new RadiusVector(5,startAngle);
                return;
            }

            var tangensOfRotationAngle = distanceBetweenPoints / lastRadiusVector.Length;
            var rotationAngel = Math.Atan(tangensOfRotationAngle);
            var lengthAddition = spiralStep / fullAngle * rotationAngel;
            var newRadiusVector = new RadiusVector(lengthAddition + lastRadiusVector.Length,
                rotationAngel + lastRadiusVector.Angle);
            lastRadiusVector = newRadiusVector;
        }

        private Point GetPointFromRadiusVector()
        {
            var alfa = lastRadiusVector.Angle;
            var r = lastRadiusVector.Length;
            var x = spiralCenter.X + r * Math.Cos(alfa);
            var y = spiralCenter.Y + r * Math.Sin(alfa);
            return new Point((int)x, (int)y);
        }

        public bool MoveNext()
        {
            GetNextRadiusVector();
            
            return true;
        }

        public void Reset()
        {
            lastRadiusVector = null;
        }

        object IEnumerator.Current => Current;

        public Point Current
        {
            get { return GetPointFromRadiusVector(); }
        }

        public void Dispose()
        {
        }
    }
}