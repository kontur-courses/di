﻿using System;
using System.Drawing;
using System.Linq;

namespace CloudLayouters
{
    public class RectangleLayouter : BaseCloudLayouter
    {
        public readonly Point Location = new Point(0, 0);

        public RectangleLayouter()
        {
            Name = "Прямоугольное облако";
            Container.AddFreePoint(Location);
        }

        public RectangleLayouter(Point location)
        {
            Location = location;
            Name = "Прямоугольное облако";
            Container.AddFreePoint(location);
        }

        public override Rectangle PutNextRectangle(Size rectangleSize)
        {
            foreach (var point in Container.GetFreePoints().OrderBy(x => Math.Max(x.X, x.Y)))
            {
                var rectangle = new Rectangle(point, rectangleSize);
                if (!CouldPutRectangle(rectangle)) continue;
                AddFreePoints(new Point(point.X + rectangleSize.Width, point.Y));
                AddFreePoints(new Point(point.X, point.Y + rectangleSize.Height));
                Container.AddRectangle(rectangle);
                return rectangle;
            }

            throw new Exception("free points not found:(");
        }

        private void AddFreePoints(Point point)
        {
            Container.AddFreePoint(point);
        }
    }
}