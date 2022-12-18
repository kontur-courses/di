using System.Collections.Generic;

namespace TagsCloudContainer.CounterNamespace
{
    public interface ICounter<T>
    {
        public IEnumerable<T> GetMostPopular(int count);
        public int GetAmount(T item);
    }
}