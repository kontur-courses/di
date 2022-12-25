using TagsCloudContainer.Core.Extensions;
using TagsCloudContainer.Core.WordsParser.Interfaces;

namespace TagsCloudContainer.Core.WordsParser
{
    public class WordsAnalyzer : IWordsAnalyzer
    {
        private readonly IWordsFilter _filter;
        private readonly IWordsReader _wordReader;

        public WordsAnalyzer(IWordsFilter filter, IWordsReader wordReader)
        {
            _filter = filter;
            _wordReader = wordReader;
        }

        public Dictionary<string, int> AnalyzeWords()
        {         
            var words = new List<string>();
            var wordsCount = new Dictionary<string, int>();

            while (true)
            {
                var word = _wordReader.ReadWord();

                if (word is null)
                    break;

                words.Add(word.ToLower());
            }

            var filteredWords = _filter.RemoveBoringWords(words.ToHashSet());


            foreach (var fWord in filteredWords.Where(fWord => words.Contains(fWord)))
            {
                wordsCount.SetOrUpdate(fWord);
            }

            return wordsCount;
        }
    }
}
