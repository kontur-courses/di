using System.Collections.Generic;
using TagsCloudContainer.TokensAndSettings;

namespace TagsCloudContainer.WordCounters
{
    public interface IWordCounter
    {
        List<WordToken> CountWords(ProcessedWord[] words);
    }
}
