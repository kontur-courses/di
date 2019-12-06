using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization.Visualizer
{
    public class CircularDrawableCloudLayouter : IDrawableProvider<string>
    {
        private readonly Point center;
        public readonly List<Rectangle> Rectangles;
        private readonly ISpiral spiralPointer;

        public CircularDrawableCloudLayouter(Point center, ISpiral spiral, IEnumerable<Sizable<string>> sizableSource)
        {
            Rectangles = new List<Rectangle>();
            spiralPointer = new FermaSpiral(center);
            this.center = center;
            DrawableObjects = GetDrawableObjects(sizableSource).ToArray();
        }


        private Point LeftDownPointOfCloud
        {
            get
            {
                if (Rectangles.Count == 0)
                    return new Point(0, 0);

                var minX = Rectangles.Select(r => r.X).Min();
                var minY = Rectangles.Select(r => r.Y).Min();
                return new Point(minX, minY);
            }
        }

        private Point RightUpperPointOfCloud
        {
            get
            {
                if (Rectangles.Count == 0)
                    return new Point(0, 0);

                var maxX = Rectangles.Select(r => r.X + r.Width).Max();
                var maxY = Rectangles.Select(r => r.Y + r.Height).Max();
                return new Point(maxX, maxY);
            }
        }

        public IEnumerable<Drawable<string>> DrawableObjects { get; }

        public Size SizeOfCloud =>
            new Size(RightUpperPointOfCloud.X - LeftDownPointOfCloud.X,
                RightUpperPointOfCloud.Y - LeftDownPointOfCloud.Y);

        private IEnumerable<Drawable<string>> GetDrawableObjects(IEnumerable<Sizable<string>> objects)
        {
            return objects.Select(sizable => new Drawable<string>(sizable.Value, PutNextRectangle(sizable.DrawSize)));
        }

        private Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.IsEmpty || rectangleSize.Height <= 0 || rectangleSize.Width <= 0)
                throw new ArgumentException("Rectangle does not exist");

            var rect = new Rectangle(spiralPointer.GetSpiralCurrent(), rectangleSize);
            while (Rectangles.Any(currentR => currentR.IntersectsWith(rect)))
            {
                var currentPoint = spiralPointer.GetSpiralNext();
                rect.X = currentPoint.X;
                rect.Y = currentPoint.Y;
            }

            rect = GetRectangleMovedToCenter(rect);

            Rectangles.Add(rect);
            return rect;
        }

        private Rectangle GetRectangleMovedToCenter(Rectangle rect)
        {
            while (TryMoveToCenter(rect, out rect)) ;
            return rect;
        }

        private bool TryMoveToCenter(Rectangle rect, out Rectangle rectOut)
        {
            {
                if (rect.X == center.X && rect.Y == center.Y)
                {
                    rectOut = rect;
                    return false;
                }

                var dx = center.X - rect.X;
                var dy = center.Y - rect.Y;

                rectOut = rect;
                var canMoveX = dx != 0 &&
                               (dx > 0
                                   ? TryMove(rect, 1, 0, out rectOut)
                                   : TryMove(rect, -1, 0, out rectOut));
                var canMoveY = dy != 0 &&
                               (dy > 0
                                   ? TryMove(rectOut, 0, 1, out rectOut)
                                   : TryMove(rectOut, 0, -1, out rectOut));
                return canMoveX || canMoveY;
            }
        }

        private bool TryMove(Rectangle rect, int dx, int dy, out Rectangle rectOut)
        {
            {
                rect.X += dx;
                rect.Y += dy;
                rectOut = rect;
                if (!Rectangles.Any(r => r.IntersectsWith(rect)))
                    return true;

                rectOut.X -= dx;
                rectOut.Y -= dy;
                return false;
            }
        }
    }
}