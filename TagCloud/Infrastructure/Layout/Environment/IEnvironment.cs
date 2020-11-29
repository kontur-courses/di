using System.Collections.Generic;

namespace TagCloud.Infrastructure.Layout.Environment
{
    public interface IEnvironment<T> : ICollisionDetector<T>, IEnumerable<T>
    {
        public void Add(T item);
    }
}