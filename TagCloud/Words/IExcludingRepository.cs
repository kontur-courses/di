using System.Collections.Generic;

namespace TagCloud.Interfaces
{
    public interface IExcludingRepository
    {
        void Load(IEnumerable<string> words);
        bool Contains(string word);
    }
}