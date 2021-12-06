﻿using System;
using System.Drawing;

namespace TagsCloudContainer.Layouter.PointsCreators
{
    public class SpiralPointsProvider : IPointsProvider
    {
        private const double AngleDelta = Math.PI / 360;

        private readonly Point center;
        
        private double currentAngle;
        private Point? lastPoint;

        public SpiralPointsProvider(Point center)
        {
            this.center = center;
        }

        public Point GetNextPoint()
        {
            Point currentPoint;
            do
            {
                var radiusVector = currentAngle;
                var newX = center.X + radiusVector * Math.Cos(currentAngle);
                var newY = center.Y + radiusVector * Math.Sin(currentAngle);
                var roundedX = (int) Math.Round(newX);
                var roundedY = (int) Math.Round(newY);
                currentPoint = new Point(roundedX, roundedY);
                currentAngle += AngleDelta;
            } while (currentPoint == lastPoint);

            lastPoint = currentPoint;
            return currentPoint;
        }
    }
}