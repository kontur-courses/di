using System.Collections;
using System.Collections.Generic;

namespace TagsCloudContainer.Layout
{
    public class FrequencyDictionary<T> : IEnumerable<KeyValuePair<T, int>>
    {
        private readonly Dictionary<T, int> dictionary = new();

        public FrequencyDictionary() { }

        public FrequencyDictionary(IEnumerable<T> items)
        {
            AddRange(items);
        }

        public void Add(T item)
        {
            if (dictionary.ContainsKey(item))
                dictionary[item]++;
            else
                dictionary.Add(item, 1);
        }

        public int this[T item] => dictionary.TryGetValue(item, out var value) ? value : 0;

        public void AddRange(IEnumerable<T> list)
        {
            foreach (var item in list)
                Add(item);
        }

        public Dictionary<T, int> ToDictionary() => dictionary;

        public IEnumerable<T> Keys => dictionary.Keys;

        public IEnumerable<int> Values => dictionary.Values;

        public IEnumerator<KeyValuePair<T, int>> GetEnumerator() => dictionary.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}