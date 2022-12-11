using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.App.Layouter
{
    public class CircularCloudLayouter : ICircularCloudLayouter
    {
        public List<Rectangle> Rectangles { get; set; }
        private int lastNumberPoint;
        private readonly CloudLayouterSettings settings;

        public CircularCloudLayouter(CloudLayouterSettings cloudLayouterSettings)
        {
            if (cloudLayouterSettings.Center.X < 0 || cloudLayouterSettings.Center.Y < 0)
                throw new ArgumentException();
            this.settings = cloudLayouterSettings;
            Rectangles = new List<Rectangle>();
            lastNumberPoint = 0;
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            Rectangle rect;
            for (; ; lastNumberPoint++)
            {
                var phi = lastNumberPoint * settings.SpiralStep;
                var r = settings.OffsetPoint * lastNumberPoint;
                var x = (int)(r * Math.Cos(phi)) + settings.Center.X;
                var y = (int)(r * Math.Sin(phi)) + settings.Center.Y;
                var point = new Point(x - rectangleSize.Width / 2, y - rectangleSize.Height / 2);
                rect = new Rectangle(point, rectangleSize);
                if (!rect.AreIntersected(Rectangles))
                {
                    if (settings.IsOffsetToCenter) rect = OffsetToCenter(rect);
                    break;
                }
            }
            Rectangles.Add(rect);
            return rect;
        }

        public void Clear()
        {
            Rectangles = new List<Rectangle>();
            lastNumberPoint = 0;
        }

        private Rectangle OffsetToCenter(Rectangle rect)
        {
            var point = rect.Location;
            while (rect.CanBeShiftedToPointX(settings.Center))
            {
                var newX = ((rect.Center().X < settings.Center.X) ? 1 : -1) + point.X;
                var pointNew = new Point(newX, point.Y);
                var rectNew = new Rectangle(pointNew, rect.Size);
                if (rectNew.AreIntersected(Rectangles)) break;
                point = pointNew;
                rect = rectNew;
            }
            while (rect.CanBeShiftedToPointY(settings.Center))
            {
                var newY = ((rect.Center().Y < settings.Center.Y) ? 1 : -1) + point.Y;
                var pointNew = new Point(point.X, newY);
                var rectNew = new Rectangle(pointNew, rect.Size);
                if (rectNew.AreIntersected(Rectangles)) break;
                point = pointNew;
                rect = rectNew;
            }
            return rect;
        }
    }
}
