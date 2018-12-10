using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.BoringWordsGetters
{
    public class PrepositionAndPronounsGetter: IBoringWordsGetter
    {
        private readonly string[] prepositions = 
            {"a", "an", "the", "or", "and", "in", "on", "at", "under", "after", "before"};

        private readonly string[] pronouns = 
            {"I", "you", "they", "he", "it", "she", "his", "her", "him", "their"};

        public IEnumerable<string> GetBoringWords()
            => prepositions.Concat(pronouns);
    }
}