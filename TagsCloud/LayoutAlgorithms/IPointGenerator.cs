using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloud.LayoutAlgorithms
{
    public interface IPointGenerator
    {
        public Point GetNextPoint();
    }
}
