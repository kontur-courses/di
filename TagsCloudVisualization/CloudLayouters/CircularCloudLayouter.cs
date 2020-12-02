using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter : ICloudLayout
    {
        public List<ICloudTag> Rectangles { get; }
        private IPointProvider pointProvider;
        private IConfig config;

        public CircularCloudLayouter(IPointProvider pointProvider, IConfig config)
        {
            this.pointProvider = pointProvider;
            this.config = config;
            Rectangles = new List<ICloudTag>();
        }

        public Rectangle PutNextRectangle(Size rectangleSize, string text)//TODO Remove text
        {
            if (rectangleSize.Height < 0 || rectangleSize.Width < 0)
                throw new ArgumentException("Width or height of rectangle was negative");

            var rectangle = GetRectangle(rectangleSize, pointProvider, config);
            Rectangles.Add(new CloudTag(rectangle, text));

            return rectangle;
        }

        private Rectangle GetRectangle(Size rectangleSize, IPointProvider pointProvider, IConfig config)
        {
            Rectangle rectangle;
            do
            {
                rectangle = new Rectangle(pointProvider.GetPoint(config), rectangleSize);

            } while (IsCollide(rectangle));

            return rectangle;
        }

        private bool IsCollide(Rectangle rectangle)
        {
            return Rectangles.Select(x => x.Rectangle).Any(rectangle.IntersectsWith) 
                   || rectangle.X < 0 || rectangle.Y < 0;
            
        }
    }
}

