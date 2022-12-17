using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Distribution
{
    public interface IDistribution
    {
        public IEnumerable<Point> GetPoints();
    }
}