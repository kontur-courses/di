using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public class TagCloudLayouter
    {
        public Point Center => generator.GetCenter();
        private readonly IPositionGenerator generator;
        private readonly List<Rectangle> rectangles = new List<Rectangle>();

        public IEnumerable<Rectangle> Rectangles => rectangles;

        public TagCloudLayouter(IPositionGenerator generator)
        {
            this.generator = generator;
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            Rectangle rect;
            do
                rect = new Rectangle(generator.GetNextPosition(), rectangleSize);
            while (rect.IntersectsWithAnyFrom(rectangles));
            
            rectangles.Add(rect);
            return rect;
        }
    }
}