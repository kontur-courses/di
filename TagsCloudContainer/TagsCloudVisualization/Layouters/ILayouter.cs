using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.Layouters
{
    public interface ILayouter
    {
        public IEnumerable<RectangleF> PlacedRectangles { get; }
        public RectangleF PutNextRectangle(SizeF rectangleSize);
        public IEnumerable<RectangleF> PutNextRectangles(IEnumerable<SizeF> rectanglesSizes);
    }
}