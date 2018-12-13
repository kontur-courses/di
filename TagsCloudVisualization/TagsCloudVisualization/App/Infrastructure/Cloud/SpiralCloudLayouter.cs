using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class SpiralCloudLayouter : ICloudLayouter
    {
        private Point center;
        private double angle;
        private ISizeDefiner sizeDefiner;
        private readonly List<Rectangle> rectangles = new List<Rectangle>();

        public SpiralCloudLayouter(ISizeDefiner sizeDefiner)
        {
            this.sizeDefiner = sizeDefiner;
        }

        public void Clear()
        {
            rectangles.Clear();
            angle = 0;
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
            var radius = 0;
            var size = sizeDefiner.GetSize(graphicWord);
            Rectangle rectangle;
            do
            {
                rectangle = new Rectangle(
                    center.X - (size.Width / 2) + (int)(radius * Math.Cos(angle)),
                    center.Y - (size.Height / 2) + (int)(radius * Math.Sin(angle)),
                    size.Width, size.Height);

                radius++;
            } while (CheckCollisionWithAll(rectangle));

            angle += 0.4;

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
