using System.Drawing;

namespace TagsCloudVisualisation.Layouting
{
    public class CircularTagCloudLayouterFactory : ILayouterFactory
    {
        public ITagCloudLayouter Create(Point centerPoint, Size minDistance) =>
            new CircularTagCloudLayouter(centerPoint, minDistance);
    }
}