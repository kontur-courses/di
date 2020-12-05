using System.Drawing;

namespace TagCloud.Core.Layouting
{
    public interface ILayouterFactory
    {
        ITagCloudLayouter Create(Point centerPoint, Size minDistance);
    }
}