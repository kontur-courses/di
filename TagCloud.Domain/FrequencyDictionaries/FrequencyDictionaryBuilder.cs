public class FrequencyDictionaryBuilder<T> : IFrequencyDictionaryBuilder<T>
{
    public Dictionary<T, int> Build(T[] items)
    {
        var dict = new Dictionary<T, int>();

        foreach (var item in items)
        {
            dict.TryAdd(item, 0);
            dict[item]++;
        }

        return dict;
    }
}