using System.Drawing;

namespace TagsCloudContainer;

public interface IRectanglesDistributor
{
    public Result<Dictionary<string, Rectangle>> DistributedRectangles { get; }
}