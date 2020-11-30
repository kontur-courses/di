using System;
using System.Drawing;

namespace HomeExerciseTDD
{
    public class Spiral : ISpiral
    {
        private Point center;
        private readonly float step;
        private Point? previousPoint;
        private float angle;
        
        public Spiral(Point center, float step = 0.0005f, float angle = 0f) 
        //public Spiral(SpiralSettings settings)
        {
            this.step = step;
            this.angle = angle;
            previousPoint = null;
            this.center = center;
        }

        public Point GetNextPoint()
        {
            while (true)
            {
                if (previousPoint == null)
                {
                    previousPoint = center;
                    return center;
                }
                
                angle++;
                var currentPoint = CalculateCurrentPoint();
                
                if (previousPoint.Equals(currentPoint))
                    continue;
                
                previousPoint = currentPoint;
                return currentPoint;
            }
        }

        private Point CalculateCurrentPoint()
        {
            var distance = angle * step;
            var currentX = distance * (float) Math.Cos(angle) + center.X;
            var currentY = distance * (float) Math.Sin(angle) + center.Y;
            
            return new Point((int) currentX, (int) currentY);
        }
    }
}