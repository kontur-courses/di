using System.Drawing;
using TagsCloudContainer;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private readonly Spiral spiral;
        private readonly Surface surface = new Surface();
        
        public Point Center { get; }

        public CircularCloudLayouter(Point center, Spiral spiral)
        {
            Center = center;
            this.spiral = spiral;
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var rect = new Rectangle(spiral.GetNextPoint(), rectangleSize);
            while (surface.RectangleIntersectsWithOther(rect))
            {
                rect.Location = spiral.GetNextPoint();
            }

            if (rect.Location != Point.Empty)
                rect = surface.GetShiftedToCenterRectangle(rect);

            surface.AddRectangle(rect);
            spiral.Reset();
            return rect;
        }
    }
}