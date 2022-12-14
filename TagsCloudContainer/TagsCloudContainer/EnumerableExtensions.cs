namespace TagsCloudContainer;

public static class EnumerableExtensions
{
    public static void ForeachWithCounterFromZero<T>(this IEnumerable<T> enumerable, Action<T, int> action)
    {
        var counter = 0;
        foreach (var item in enumerable)
        {
            action(item, counter);
            counter++;
        }
    }
}