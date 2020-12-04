using System.Drawing;

namespace TagsCloudVisualisation.Layouting
{
    public interface ILayouterFactory
    {
        ITagCloudLayouter Get(Point centerPoint, Size minDistance);
    }
}