using System.Collections.Generic;

namespace TagsCloudContainer.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddOrIncreaseCount<TKey>(this Dictionary<TKey, int> dict, TKey key)
        {
            if (dict.ContainsKey(key))
                dict[key] += 1;
            else
                dict[key] = 1;
        }
    }
}