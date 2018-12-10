using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.BoringWordsGetters
{
    public interface IBoringWordsGetter
    {
        IEnumerable<string> GetBoringWords();
    }
}