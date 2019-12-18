using System.Drawing;
using TagsCloudVisualization.Spirals;

namespace TagsCloudVisualization.TagCloudLayouter
{
    public class LayouterFactory
    {
        public ITagCloudLayouter GetCircularLayouter(Point center)
        {
            return new CircularCloudLayouter(center);
        }
        
        public ITagCloudLayouter GetCircularLayouter(Point center, ISpiral spiral)
        {
            return new CircularCloudLayouter(center, spiral);
        }
    }
}