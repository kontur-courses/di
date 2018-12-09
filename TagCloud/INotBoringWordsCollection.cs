using System.Collections.Generic;

namespace TagsCloud
{
    public interface IBoringWordsCollection
    {
        IEnumerable<string> DeleteBoringWords();
    }
}