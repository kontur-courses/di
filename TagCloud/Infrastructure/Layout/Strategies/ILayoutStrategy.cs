using System;
using System.Drawing;

namespace TagCloud.Infrastructure.Layout.Strategies
{
    public interface ILayoutStrategy
    {
        Point GetPoint(Func<Point, bool> isValidPoint);
    }
}