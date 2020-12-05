using System.Drawing;

namespace TagCloud.Core.Layouting
{
    public interface ILayouterFactory
    {
        TagCloudLayouterType Type { get; }
        ITagCloudLayouter Create(Point centerPoint, Size minDistance);
    }
}