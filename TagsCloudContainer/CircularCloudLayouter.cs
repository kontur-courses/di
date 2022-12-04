using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloudContainer
{
    public class CircularCloudLayouter 
    {
        public List<Rectangle> Rectangles { get; set; }
        public readonly Point Center;
        private readonly double offsetPoint;
        private readonly double spiralStep;
        private int lastNumberPoint;
        public bool IsOffsetToCenter { get; set; }

        public CircularCloudLayouter(Point center, bool isOffsetToCenter, double offsetPoint, double spiralStep)
        {
            if (center.X < 0 || center.Y < 0) throw new ArgumentException();
            this.spiralStep = spiralStep;
            this.offsetPoint = offsetPoint;
            Center = center;
            IsOffsetToCenter = isOffsetToCenter;
            Rectangles = new List<Rectangle>();
            lastNumberPoint = 0;
        }

        public CircularCloudLayouter(Point center) : this(center, false, 0.01, -0.3)
        {
        }

        public CircularCloudLayouter(Point center, bool isOffsetToCenter) : this(center, isOffsetToCenter, 0.01, -0.3)
        {
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            Rectangle rect;
            for (; ; lastNumberPoint++)
            {
                var phi = lastNumberPoint * spiralStep;
                var r = offsetPoint * lastNumberPoint;
                var x = (int)(r * Math.Cos(phi)) + Center.X;
                var y = (int)(r * Math.Sin(phi)) + Center.Y;
                var point = new Point(x - rectangleSize.Width / 2, y - rectangleSize.Height / 2);
                rect = new Rectangle(point, rectangleSize);
                if (!rect.AreIntersected(Rectangles))
                {
                    if (IsOffsetToCenter) rect = OffsetToCenter(rect);
                    break;
                }
            }
            Rectangles.Add(rect);
            return rect;
        }

        private Rectangle OffsetToCenter(Rectangle rect)
        {
            var point = rect.Location;
            while (rect.CanBeShiftedToPointX(Center))
            {
                var newX = ((rect.Center().X < Center.X) ? 1 : -1) + point.X;
                var pointNew = new Point(newX, point.Y);
                var rectNew = new Rectangle(pointNew, rect.Size);
                if (rectNew.AreIntersected(Rectangles)) break;
                point = pointNew;
                rect = rectNew;
            }
            while (rect.CanBeShiftedToPointY(Center))
            {
                var newY = ((rect.Center().Y < Center.Y) ? 1 : -1) + point.Y;
                var pointNew = new Point(point.X, newY);
                var rectNew = new Rectangle(pointNew, rect.Size);
                if (rectNew.AreIntersected(Rectangles)) break;
                point = pointNew;
                rect = rectNew;
            }
            return rect;
        }

        public void SaveBitmap(string btmName)
        {
            var bmp = new Bitmap(800, 500);
            using Graphics gph = Graphics.FromImage(bmp);
            var blackPen = new Pen(Color.Black, 1);
            gph.DrawRectangles(blackPen, Rectangles.ToArray());
            bmp.Save(btmName + ".bmp");
        }
    }
}
