using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloud.CloudStructure
{
    public interface IPointGenerator
    {
        IEnumerable<Point> GeneratePoints(Point center);
    }
}
