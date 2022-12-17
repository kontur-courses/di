using System.Collections.Generic;

namespace TagsCloudContainer.Counter
{
    public interface ICounter<T>
    {
        public IEnumerable<T> GetMostPopular(int count);
        public int GetAmount(T item);
    }
}