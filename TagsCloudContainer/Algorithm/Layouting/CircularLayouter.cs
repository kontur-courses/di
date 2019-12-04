using System.Drawing;
using TagsCloudVisualization;

namespace TagsCloudContainer.Algorithm.Layouting
{
    public class CircularLayouter : ILayouter
    {
        private readonly CircularCloudLayouter circularCloudLayouter;

        public CircularLayouter(Point center, Size pictureSize)
        {
            circularCloudLayouter = new CircularCloudLayouter(center, pictureSize);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            return circularCloudLayouter.PutNextRectangle(rectangleSize);
        }
    }
}