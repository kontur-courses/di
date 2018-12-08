using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordsProcessing
{
    public class TextPreprocessor : IWordsProvider
    {
        private readonly IEnumerable<string> words;
        private readonly IFilter wordsFilter;
        private readonly IWordsChanger wordsChanger;


        public TextPreprocessor(IWordsProvider wordsProvider, IFilter wordsFilter, IWordsChanger wordsChanger)
        {
            words = wordsProvider.GetWords();
            this.wordsFilter = wordsFilter;
            this.wordsChanger = wordsChanger;
        }

        public IEnumerable<string> GetWords()
        {
            return wordsFilter.FilterWords(words.Select(word => wordsChanger.ChangeWord(word)));
        }
    }
}