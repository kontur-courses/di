﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGenerator.RectanglesLayouters
{
    public class DefaultRectanglesLayouter : IRectanglesLayouter
    {
        private readonly List<RectangleF> rectangles;
        private readonly IPointsSearcher searcher;

        public DefaultRectanglesLayouter(IPointsSearcher searcher)
        {
            this.searcher = searcher ?? throw new ArgumentNullException(nameof(searcher));
            rectangles = new List<RectangleF>();
        }

        public RectangleF PutNextRectangle(SizeF rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentOutOfRangeException();

            var rectangle = new RectangleF { Size = rectangleSize };
            rectangle.Location = FindFreeLocation(rectangle);
            rectangles.Add(rectangle);

            return rectangle;
        }

        private PointF FindFreeLocation(RectangleF rectangle)
        {
            do rectangle = rectangle.SetCenter(searcher.GetNextPoint());
            while (rectangles.Any(r => r.IntersectsWith(rectangle)));
            return rectangle.Location;
        }
    }

    internal static class RectangleExtensions
    {
        public static RectangleF SetCenter(this RectangleF rectangle, PointF center)
        {
            rectangle.Location = new PointF(center.X - rectangle.Width / 2, center.Y - rectangle.Height / 2);
            return rectangle;
        }
    }
}