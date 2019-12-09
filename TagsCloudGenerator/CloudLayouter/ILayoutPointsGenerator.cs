using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudGenerator.CloudLayouter
{
    public interface ILayoutPointsGenerator
    {
        IEnumerable<Point> GetPoints();
    }
}