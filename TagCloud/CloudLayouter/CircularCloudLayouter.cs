using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.CloudVisualizer.CloudViewConfiguration;
using TagCloud.FigurePaths;

namespace TagCloud.CloudLayouter
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        public IEnumerable<Rectangle> Rectangles => rectangles; 

        private readonly List<Rectangle> rectangles;

        private CloudViewConfiguration configuration;
        private IFigurePath figurePath;


        public CircularCloudLayouter(CloudViewConfiguration configuration)
        {
            this.configuration = configuration;
            rectangles = new List<Rectangle>();
            figurePath = configuration.FigurePath.GetFigurePath();
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var center = new Size(configuration.CloudCenter); // Сделано чтобы проще прибавлять к точке
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException("Directions should be non-negative");

            Rectangle rectangle = new Rectangle(figurePath.GetNextPoint() + center, rectangleSize);

            while (rectangles.Any(rect => rect.IntersectsWith(rectangle)))
            {
                rectangle = new Rectangle(figurePath.GetNextPoint() + center, rectangleSize);
            }

            if (configuration.NeedSnuggle)
                rectangle = SnuggleRectangle(rectangle);

            rectangles.Add(rectangle);

            return rectangle;
        }

        private Rectangle SnuggleRectangle(Rectangle rectangle)
        {
            var deltaX = Math.Sign(configuration.CloudCenter.X - rectangle.X);
            var deltaY = Math.Sign(configuration.CloudCenter.Y - rectangle.Y);
            while (deltaX != 0 || deltaY != 0)
            {
                rectangle.X += deltaX;
                if (deltaX != 0 && !rectangles.Any(rect => rect.IntersectsWith(rectangle)))
                {
                    deltaX = Math.Sign(configuration.CloudCenter.X - rectangle.X);
                    continue;
                }

                rectangle.X -= deltaX;
                rectangle.Y += deltaY;
                if (deltaY != 0 && !rectangles.Any(rect => rect.IntersectsWith(rectangle)))
                {
                    deltaY = Math.Sign(configuration.CloudCenter.Y - rectangle.Y);
                    continue;
                }

                rectangle.Y -= deltaY;
                break;
            }

            return rectangle;
        }
    }
}
