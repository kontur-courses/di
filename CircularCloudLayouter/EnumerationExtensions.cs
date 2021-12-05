namespace CloudLayouter;

internal static class EnumerationExtensions
{
    public static TSource? MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, TSource? def = default)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        if (selector == null)
            throw new ArgumentNullException(nameof(selector));

        using var sourceIterator = source.GetEnumerator();
        if (!sourceIterator.MoveNext())
            return def;

        var min = sourceIterator.Current;
        var minKey = selector(min);
        while (sourceIterator.MoveNext())
        {
            var candidate = sourceIterator.Current;
            var candidateProjected = selector(candidate);
            if (Comparer<TKey>.Default.Compare(candidateProjected, minKey) < 0)
            {
                min = candidate;
                minKey = candidateProjected;
            }
        }

        return min;
    }
}
