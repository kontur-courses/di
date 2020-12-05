using System.Drawing;

namespace TagCloud.Core.Layouting
{
    public class CircularTagCloudLayouterFactory : ILayouterFactory
    {
        public ITagCloudLayouter Create(Point centerPoint, Size minDistance) =>
            new CircularTagCloudLayouter(centerPoint, minDistance);
    }
}