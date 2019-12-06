using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Core
{
    class FrequencyDictionary<T>
    {
        private readonly Dictionary<T, int> frequencyDictionary;
        public FrequencyDictionary()
        {
            frequencyDictionary = new Dictionary<T, int>();
        }

        public FrequencyDictionary(IEnumerable<T> keys) : this()
        {
            foreach (var key in keys)
                Add(key);
        }

        public void Add(T key)
        {
            if (!frequencyDictionary.ContainsKey(key))
                frequencyDictionary[key] = 0;
            frequencyDictionary[key]++;
        }

        public int GetCounter(T key) => frequencyDictionary.TryGetValue(key, out var result) ? result : 0;

        public IEnumerable<(T, int)> Top(int count)
        {
            return frequencyDictionary
                .OrderByDescending(kvp => kvp.Value)
                .Take(Math.Min(count, frequencyDictionary.Count))
                .Select(kvp => (kvp.Key, kvp.Value));
        }

        public int Count() => frequencyDictionary.Count;

    }
}
