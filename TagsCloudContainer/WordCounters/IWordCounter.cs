using System.Collections.Generic;
using TagsCloudContainer.TokensAndSettings;

namespace TagsCloudContainer.WordCounters
{
    interface IWordCounter
    {
        List<WordToken> CountWords(string[] words);
    }
}
