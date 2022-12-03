using System.Drawing;

namespace TagsCloudContainer;

public interface IRectanglesDistributor
{
    public Dictionary<string,Rectangle> DistributedRectangles { get; }
}