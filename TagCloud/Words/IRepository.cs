using System.Collections.Generic;

namespace TagCloud.Interfaces
{
    public interface IRepository
    {
        void Load(IEnumerable<string> words);
        IEnumerable<string> Get();
    }
}