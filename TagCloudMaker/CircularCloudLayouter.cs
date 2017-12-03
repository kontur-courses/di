using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloudMaker
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private readonly List<TextRectangle> rectangles;
        public TextRectangle[] Cloud => rectangles.ToArray();
        private readonly SpiralPointComputer pointComputer;

        public CircularCloudLayouter()
        {
            rectangles = new List<TextRectangle>();
            pointComputer = new SpiralPointComputer();
        }

        public void PutNextRectangle(Size rectangleSize, string text)
        {
            var nextRectangle = GetNextRectangle(rectangleSize, text);
            while (rectangles.Any(tr => tr.Rectangle.IntersectsWith(nextRectangle.Rectangle)))
                nextRectangle = GetNextRectangle(rectangleSize, text);

            rectangles.Add(nextRectangle);
        }
        
        private TextRectangle GetNextRectangle(Size rectangleSize, string text)
        {
            var location =
                    (rectangles.Count == 0 ? pointComputer.GetNextPoint(0, 0) : pointComputer.GetNextPoint(0.1, 50))
                    .GetLeftTopCorner(rectangleSize);
            return new TextRectangle(location, rectangleSize, text);
        }
    }
}