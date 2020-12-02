using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud.Layouter.Factory
{
    public interface IRectanglesLayoutersFactory
    {
        IRectanglesLayoutersFactory Register(string layouterName, Func<Point, IRectanglesLayouter> creationFunc);
        IRectanglesLayouter Create();
        IEnumerable<string> GetLayouterNames();
    }
}
