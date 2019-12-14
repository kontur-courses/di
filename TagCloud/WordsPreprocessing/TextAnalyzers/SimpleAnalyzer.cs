using System.Collections.Generic;
using System.Linq;
using TagCloud.WordsPreprocessing.WordsSelector;

namespace TagCloud.WordsPreprocessing.TextAnalyzers
{
    /// <summary>
    /// Just counts words frequency
    /// </summary>
    [Name("SimpleAnalyzer")]
    public class SimpleAnalyzer : ITextAnalyzer
    {
        private readonly WordSelectorSettings wordSelectorSettings;

        public SimpleAnalyzer(WordSelectorSettings wordSelectorSettings)
        {
            this.wordSelectorSettings = wordSelectorSettings;
        }

        public Word[] GetWords(IEnumerable<string> words, int count)
        {
            var countedWords = new Dictionary<string, Word>();
            var wordsCounter = 0;

            foreach (var word in words)
            {
                if (!countedWords.ContainsKey(word))
                    countedWords[word] = new Word(word, SpeechPart.Noun);

                countedWords[word].Count++;
                wordsCounter++;
            }

            return countedWords
                .OrderByDescending(w => w.Value.Count)
                .Where(w => wordSelectorSettings.CanUseThisWord(w.Value))
                .Take(count)
                .Select(w =>
                {
                    w.Value.Frequency = (double)w.Value.Count / wordsCounter;
                    return w.Value;
                })
                .ToArray();
        }
    }
}