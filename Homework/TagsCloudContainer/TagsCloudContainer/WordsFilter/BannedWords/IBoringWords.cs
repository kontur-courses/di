using System.Collections.Generic;

namespace TagsCloudContainer.WordsFilter.BannedWords
{
    public interface IBoringWords
    {
        HashSet<string> GetBoringWords { get; }
    }
}
