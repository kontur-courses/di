using System;

namespace TagCloud.Infrastructure.Layout.Strategies
{
    public interface ILayoutStrategy<out T>
    {
        T GetPoint(Func<T, bool> isValidPoint);
    }
}