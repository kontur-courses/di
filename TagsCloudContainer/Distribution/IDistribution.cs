using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public interface IDistribution
    {
        IEnumerable<Point> GetPoints();
    }
}