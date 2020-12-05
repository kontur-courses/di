using System.Drawing;

namespace TagsCloudVisualisation.Layouting
{
    public interface ILayouterFactory
    {
        ITagCloudLayouter Create(Point centerPoint, Size minDistance);
    }
}