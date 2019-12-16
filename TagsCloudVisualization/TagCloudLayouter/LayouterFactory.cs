using System.Drawing;

namespace TagsCloudVisualization.TagCloudLayouter
{
    public class LayouterFactory
    {
        public ITagCloudLayouter GetCircularLayouter(Point center)
        {
            return new CircularCloudLayouter(center);
        }
    }
}