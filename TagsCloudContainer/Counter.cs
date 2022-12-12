using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer
{
    public class Counter<T> 
    {
        private IEnumerable<T> Enumerable { get; }
        private Dictionary<T, int> Dictionary { get; }

        public Counter(IEnumerable<T> enumerable)
        {
            Enumerable = enumerable;
            Dictionary = new Dictionary<T, int>();
            Count();
        }

        private void Count()
        {
            foreach (var item in Enumerable)
            {
                if (!Dictionary.ContainsKey(item))
                {
                    Dictionary.Add(item, 0);
                }
                Dictionary[item] += 1;
            }
        }

        public IEnumerable<(T Item, int Amount)> GetMostPopular(int count = -1)
        {
            if (count == -1) 
                count = Dictionary.Count;
            return Dictionary
                .OrderByDescending(item => item.Value)
                .Select(item => ValueTuple.Create(item.Key, item.Value))
                .Take(count);
        }
    }
}
