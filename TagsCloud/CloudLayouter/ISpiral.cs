using System.Collections;
using System.Drawing;

namespace TagsCloud.CloudLayouter;

public interface ISpiral
{
   IEnumerable<Point> GetPoints(Point start);
}