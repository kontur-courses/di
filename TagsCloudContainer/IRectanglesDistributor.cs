using System.Drawing;

namespace TagsCloudContainer;

public interface IRectanglesDistributor
{
    public List<Rectangle> DistributedRectangles { get; }
}