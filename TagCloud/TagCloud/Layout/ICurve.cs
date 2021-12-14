using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Layout
{
    internal interface ICurve
    {
        /// <summary>
        ///     Выдает дискретные точки на кривой от последнего взятого значения до бесконечности!
        /// </summary>
        IEnumerable<Point> GetDiscretePoints(double deltaAngle = 0.01);

        void Reset();
    }
}