using System.Drawing;

namespace TagsCloudContainer.WordLayouts
{
    public interface ILayouter
    {
        IPosition GetNextPosition(IPosition position, SizeF size);
    }
}