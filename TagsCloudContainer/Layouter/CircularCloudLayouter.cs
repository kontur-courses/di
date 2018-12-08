using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Layouter
{
    public class CircularCloudLayouter : ITagCloudLayouter
    {
        public Point Center => generator.GetCenter();
        private readonly IPositionGenerator generator;
        private readonly List<Rectangle> rectangles = new List<Rectangle>();

        public IEnumerable<Rectangle> Rectangles => rectangles;

        public CircularCloudLayouter(IPositionGenerator generator)
        {
            this.generator = generator;
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            Rectangle rect;
            do
                rect = new Rectangle(generator.GetNextPosition(), rectangleSize).MoveToCenter();
            while (rect.IntersectsWithAnyFrom(rectangles));
            
            rectangles.Add(rect);
            return rect;
        }
    }
}