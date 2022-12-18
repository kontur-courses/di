using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.CounterNamespace
{
    public class Counter<T> : ICounter<T>
    {
        public Counter(IEnumerable<T> enumerable)
        {
            Enumerable = enumerable;
            Dictionary = new Dictionary<T, int>();
            Count();
        }

        private IEnumerable<T> Enumerable { get; }
        private Dictionary<T, int> Dictionary { get; }

        public IEnumerable<T> GetMostPopular(int count)
        {
            return Dictionary
                .OrderByDescending(item => item.Value)
                .Select(item => item.Key)
                .Take(count);
        }

        public int GetAmount(T item)
        {
            return Dictionary.GetValueOrDefault(item, 0);
        }

        private void Count()
        {
            foreach (var item in Enumerable)
            {
                if (!Dictionary.ContainsKey(item)) Dictionary.Add(item, 0);
                Dictionary[item] += 1;
            }
        }
    }
}