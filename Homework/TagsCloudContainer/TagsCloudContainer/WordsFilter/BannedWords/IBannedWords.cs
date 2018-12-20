using System.Collections.Generic;

namespace TagsCloudContainer.WordsFilter.BannedWords
{
    public interface IBannedWords
    {
        HashSet<string> GetBannedWords { get; }
    }
}
