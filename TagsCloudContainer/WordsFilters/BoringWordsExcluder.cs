using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.BoringWordsGetters;

namespace TagsCloudContainer.WordsFilters
{
    public class BoringWordsExcluder: IFilter<string>
    {
        private readonly HashSet<string> boringWords;
        
        public BoringWordsExcluder(IEnumerable<IBoringWordsGetter> wordsFilters)
        {
            var words = wordsFilters.SelectMany(x => x.GetBoringWords()).Select(x => x.ToLower());
            boringWords = new HashSet<string>(words);
        }

        public bool IsCorrect(string word)
            => !boringWords.Contains(word.ToLower());
    }
}