using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagCloud.PointGenerators
{
    public interface IPointGenerator
    {
        Point GetNextPoint();
        Point GetCenterPoint();
    }
}
