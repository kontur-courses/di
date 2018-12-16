using TagsCloudContainer.Dictionaries;

namespace TagsCloudContainer.WordsFilters
{
    public class UnknownWordsExcluder : IFilter<string>
    {
        private readonly IGrammarDictionary grammarDictionary;

        public UnknownWordsExcluder(IGrammarDictionary grammarDictionary)
        {
            this.grammarDictionary = grammarDictionary;
        }

        public bool IsCorrect(string value)
            => grammarDictionary.ContainsWord(value);
    }
}

