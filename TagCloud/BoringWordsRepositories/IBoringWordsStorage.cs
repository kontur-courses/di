using System.Collections.Generic;

namespace TagCloud.BoringWordsStorage
{
    public interface IBoringWordsStorage
    {
        public HashSet<string> GetBoringWords();
    }
}
