using System.Drawing;
using System.Reflection;

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