using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    public interface ISpiral
    {
        /// <summary>
        /// Выдает дискретные значения спирали от последнего взятого значения до бесконечности!
        /// </summary>
        IEnumerable<Point> GetDiscretePoints(double deltaAngle = 0.01);
    }
}
