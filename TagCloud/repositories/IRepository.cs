using System.Collections.Generic;

namespace TagCloud.repositories
{
    public interface IRepository<out TOwner>
    {
        IEnumerable<TOwner> Get();
    }
}