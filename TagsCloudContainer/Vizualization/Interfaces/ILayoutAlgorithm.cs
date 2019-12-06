using System.Drawing;

namespace TagsCloudContainer
{
    public interface ILayoutAlgorithm
    {
        Point GetNextPoint();
    }
}