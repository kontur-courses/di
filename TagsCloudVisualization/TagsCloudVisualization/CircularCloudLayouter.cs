﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Curves;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter
    {
        private readonly ICurve curve;
        private readonly List<Rectangle> rectangles;

        public CircularCloudLayouter(ICurve curve)
        {
            this.curve = curve;
            rectangles = new List<Rectangle>();
        }

        public Rectangle PutNextRectangle(Size size)
        {
            if (size.Width <= 0 || size.Height <= 0)
                throw new ArgumentException("Lengths of size must be positive");

            var nextRect = GetNextNotIntersectingRectangle(size);
            rectangles.Add(nextRect);

            return nextRect;
        }

        private Rectangle GetNextNotIntersectingRectangle(Size size)
        {
            Rectangle nextRectangle;
            do
            {
                nextRectangle = GetNextRectangle(size);
            } while (nextRectangle.IntersectsWith(rectangles));

            return nextRectangle;
        }

        private Rectangle GetNextRectangle(Size size)
        {
            var nextPoint = curve.GetNextPoint();
            return new Rectangle(nextPoint.X, nextPoint.Y, size.Width, size.Height);
        }

        public Rectangle[] GetRectangles()
        {
            return rectangles.ToArray();
        }

        public Point[] GetStartPointWords()
        {
            return rectangles.Select(r => new Point(r.Left, r.Top)).ToArray();
        }
    }
}