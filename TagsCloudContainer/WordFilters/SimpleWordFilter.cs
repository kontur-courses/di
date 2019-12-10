using TagsCloudContainer.TokensAndSettings;
using System.Collections.Generic;

namespace TagsCloudContainer.WordFilters
{
    public class SimpleWordFilter : IWordFilter
    {
        private List<string> blackList = new List<string>
        {
            "CONJ",
            "INTJ",
            "PART",
            "PR"
        };

        public bool IsCorrect(ProcessedWord word)
        {
            return !blackList.Contains(word.PartOfSpeech);
        }
    }
}
