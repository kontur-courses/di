using System.Collections.Generic;

namespace TagsCloudContainer.BoringWordsGetters
{
    public interface IBoringWordsGetter
    {
        IEnumerable<string> GetBoringWords();
    }
}