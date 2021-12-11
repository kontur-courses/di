namespace TagsCloudContainer;

public static class DictionaryExtension
{
    public static void Increment(this Dictionary<string, int> counts, string key)
    {
        counts[key] = 1 +
            (counts.TryGetValue(key, out var count)
            ? count : 0);
    }
}
