using System;

namespace TagCloud.Infrastructure.Layout
{
    public interface ILayouter<in TIn, out TOut> : IDisposable
    {
        public TOut GetPlace(TIn item);
    }
}