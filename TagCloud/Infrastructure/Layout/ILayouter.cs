using System.Collections;
using System.Collections.Generic;

namespace TagCloud.Infrastructure.Layout
{
    public interface ILayouter<in TIn, out TOut>
    {
        public TOut Layout(TIn items);
    }
}