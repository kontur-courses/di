using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Extensions;

namespace TagsCloudVisualization.Layouters
{
    public class CircularCloudLayouter : ILayouter
    {
        private readonly Point centerCoordinates;
        private readonly List<Rectangle> rectangles;
        private readonly Spiral spiral;

        public CircularCloudLayouter(Spiral spiral)
        {
            centerCoordinates = spiral.Center;
            rectangles = new List<Rectangle>();
            this.spiral = spiral;
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException("Sides of rectangle can't be negative");
            var rectangle = new Rectangle(spiral.GetNextPoint(), rectangleSize);
            if (rectangles.Count == 0)
                rectangle = rectangle.MoveToCenter(centerCoordinates);
            while (RectangleIntersect(rectangle))
                rectangle = new Rectangle(spiral.GetNextPoint(), rectangleSize);
            rectangles.Add(rectangle);

            return rectangle;
        }

        private bool RectangleIntersect(Rectangle rectangle)
        {
            return rectangles.Any(rectangle.IntersectsWith);
        }
    }
}