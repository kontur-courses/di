using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private Point center;
        private int radius;
        private ISizeDefiner sizeDefiner;
        private readonly List<Rectangle> rectangles = new List<Rectangle>();

        public CircularCloudLayouter(ISizeDefiner sizeDefiner)
        {
            this.sizeDefiner = sizeDefiner;
        }

        public void Clear()
        {
            rectangles.Clear();
            radius = 0;
        }

        public void Process(IEnumerable<GraphicWord> graphicWords, ISizeDefiner sizeDefiner, Point center)
        {
            this.sizeDefiner = sizeDefiner;
            this.center = center;

            foreach (var graphicWord in graphicWords)
            {
                PutNextWord(graphicWord);
            }
            Clear();
        }

        private void PutNextWord(GraphicWord graphicWord)
        {
            var angle = 0.0;
            var size = sizeDefiner.GetSize(graphicWord);
            Rectangle rectangle;
            do
            {
                rectangle = new Rectangle(
                    center.X - (size.Width / 2) + (int)(radius * Math.Cos(angle)),
                    center.Y - (size.Height / 2) + (int)(radius * Math.Sin(angle)),
                    size.Width, size.Height);

                angle += Math.PI / 18;
                if (angle < Math.PI * 2)
                    continue;

                angle = 0;
                radius++;
            } while (CheckCollisionWithAll(rectangle));

            if (rectangles.Count > 0)
                rectangle = MoveRectangleToCenter(rectangle);
            graphicWord.Rectangle = rectangle;
            rectangles.Add(rectangle);
        }

        private Rectangle MoveRectangleToCenter(Rectangle rectangle)
        {
            Point originalLocation;
            do
            {
                originalLocation = new Point(rectangle.X, rectangle.Y);

                if (rectangle.X + (rectangle.Width / 2) > center.X)
                    rectangle = MoveRectangleIfNoCollision(rectangle, -1, 0);

                if (rectangle.Y + (rectangle.Height / 2) > center.Y)
                    rectangle = MoveRectangleIfNoCollision(rectangle, 0, -1);

                if (rectangle.X + (rectangle.Width / 2) < center.X)
                    rectangle = MoveRectangleIfNoCollision(rectangle, 1, 0);

                if (rectangle.Y + (rectangle.Height / 2) < center.Y)
                    rectangle = MoveRectangleIfNoCollision(rectangle, 0, 1);

            } while (originalLocation != rectangle.Location);
            return rectangle;
        }

        private Rectangle MoveRectangleIfNoCollision(Rectangle rectangle, int dx, int dy)
        {
            var changed = rectangle;
            changed.X += dx;
            changed.Y += dy;

            return CheckCollisionWithAll(changed) ? rectangle : changed;
        }

        private bool CheckCollisionWithAll(Rectangle rect)
        {
            foreach (var other in rectangles)
            {
                if (Geometry.IsRectangleIntersection(rect, other))
                    return true;
            }

            return false;
        }
    }
}
