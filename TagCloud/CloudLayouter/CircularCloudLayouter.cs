using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.FigurePaths;

namespace TagCloud.CloudLayouter
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        public readonly Size center;

        public IEnumerable<Rectangle> Rectangles => rectangles; 

        private readonly IFigurePath figurePath; // теперь в классе Spiral метод возвращает просто следующую точку
        private Rectangle cloudRectangle;
        private readonly List<Rectangle> rectangles;


        public CircularCloudLayouter(IFigurePath figurePath, Point center)
        {
            rectangles = new List<Rectangle>();
            this.center = new Size(center);
            this.figurePath = figurePath;
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException("Directions should be non-negative");

            Rectangle rectangle = new Rectangle(figurePath.GetNextPoint() + center, rectangleSize);

            while (rectangles.Any(rect => rect.IntersectsWith(rectangle)))
            {
                rectangle = new Rectangle(figurePath.GetNextPoint() + center, rectangleSize);
            }

            rectangle = SnuggleRectangle(rectangle);

            rectangles.Add(rectangle);
            
            UpdateCloudRectangle(rectangle);

            return rectangle;
        }

        public Rectangle CloudRectangle => cloudRectangle;

        private Rectangle SnuggleRectangle(Rectangle rectangle)
        {
            var deltaX = Math.Sign(center.Width - rectangle.X);
            var deltaY = Math.Sign(center.Height - rectangle.Y);
            while (deltaX != 0 || deltaY != 0)
            {
                rectangle.X += deltaX;
                if (deltaX != 0 && !rectangles.Any(rect => rect.IntersectsWith(rectangle)))
                {
                    deltaX = Math.Sign(center.Width - rectangle.X);
                    continue;
                }

                rectangle.X -= deltaX;
                rectangle.Y += deltaY;
                if (deltaY != 0 && !rectangles.Any(rect => rect.IntersectsWith(rectangle)))
                {
                    deltaY = Math.Sign(center.Height - rectangle.Y);
                    continue;
                }

                rectangle.Y -= deltaY;
                break;
            }

            return rectangle;
        }

        private void UpdateCloudRectangle(Rectangle rectangle)
        {
            if (rectangle.X < CloudRectangle.X)
            {
                cloudRectangle.Width += cloudRectangle.X - rectangle.X;
                cloudRectangle.X = rectangle.X;
            }

            if (rectangle.Y < CloudRectangle.Y)
            {
                cloudRectangle.Height += cloudRectangle.Y - rectangle.Y;
                cloudRectangle.Y = rectangle.Y;
            }

            if (rectangle.Right > cloudRectangle.Right)
            {
                cloudRectangle.Width += rectangle.Right - cloudRectangle.Right;
            }

            if (rectangle.Bottom > cloudRectangle.Bottom)
            {
                cloudRectangle.Height += rectangle.Bottom - cloudRectangle.Bottom;
            }
        }
    }
}
