using System.Collections.Generic;

namespace TagsCloudContainer.WordsFilter.BoringWords
{
    public interface IBoringWords
    {
        HashSet<string> GetBoringWords { get; }
    }
}
