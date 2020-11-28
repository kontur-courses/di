﻿using System.Drawing;

 namespace CircularCloudLayouter
{
    public static class RectangleExtensions
    {
        public static Rectangle SetCenter(this Rectangle rectangle, Point centerPoint)
        {
            rectangle.X = centerPoint.X - rectangle.Width / 2;
            rectangle.Y = centerPoint.Y - rectangle.Height / 2;
            return rectangle;
        }

        public static Rectangle Shift(this Rectangle rectangle, Point shift)
        {
            rectangle.Offset(shift);
            return rectangle;
        }
    }
}