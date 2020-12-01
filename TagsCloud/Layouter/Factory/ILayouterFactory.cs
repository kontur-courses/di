using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud.Layouter.Factory
{
    public interface ILayouterFactory
    {
        void Register(string layouterName, Func<Point, ILayouter> creationFunc);
        ILayouter Create(Point center);
        IEnumerable<string> GetLayouterNames();
    }
}
