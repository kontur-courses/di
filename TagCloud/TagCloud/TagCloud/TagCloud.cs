using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud
{
    public abstract class TagCloud : ITagCloud
    {
        public TagCloud()
        {
            WordRectangles = new List<WordRectangle>();
        }

        protected abstract IWordsProvider WordsProvider { get; }

        protected abstract IWordsFilter WordsFilter { get; }

        public void GenerateTagCloud()
        {
            var words = WordsProvider.GetWords();
            var filteredWords = WordsFilter.Apply(words);
            var wordsFrequencies = GetTokens(filteredWords)
                .OrderByDescending(wordToken => wordToken.Frequency);
            var wordsCount = wordsFrequencies.Select(wordToken => wordToken.Frequency).Sum();
            foreach (var wordToken in wordsFrequencies)
            {
                var width = Math.Max(100, 500 * wordToken.Frequency / wordsCount);
                var height = Math.Max(50, 250 * wordToken.Frequency / wordsCount);
                var size = new Size(width, height);
                PutNextWord(wordToken.Word, size);
            }
        }

        public abstract WordRectangle PutNextWord(string word, Size rectangleSize);

        public List<WordRectangle> WordRectangles { get; }

        //TODO вынести в отдельный класс
        private IEnumerable<WordToken> GetTokens(IEnumerable<string> words)
        {
            var frequencies = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!frequencies.ContainsKey(word))
                    frequencies[word] = 0;
                frequencies[word]++;
            }

            return frequencies.Select(x => new WordToken(x.Key, x.Value));
        }
    }
}