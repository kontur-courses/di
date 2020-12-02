using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter : ICloudLayout
    {
        public List<ICloudTag> Rectangles { get; }
        private readonly IPointProvider pointProvider;

        public CircularCloudLayouter(IPointProvider provider)
        {
            Rectangles = new List<ICloudTag>();
            pointProvider = provider;
        }

        public Rectangle PutNextRectangle(Size rectangleSize, string text)//TODO Remove text
        {
            if (rectangleSize.Height < 0 || rectangleSize.Width < 0)
                throw new ArgumentException("Width or height of rectangle was negative");

            var rectangle = GetRectangle(rectangleSize);
            Rectangles.Add(new CloudTag(rectangle, text));

            return rectangle;
        }

        private Rectangle GetRectangle(Size rectangleSize)
        {
            Rectangle rectangle;
            do
            {
                rectangle = new Rectangle(pointProvider.GetPoint(), rectangleSize);

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

