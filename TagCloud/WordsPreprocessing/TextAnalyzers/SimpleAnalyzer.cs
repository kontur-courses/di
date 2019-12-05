using System.Collections.Generic;
using System.Linq;

namespace TagCloud.WordsPreprocessing.TextAnalyzers
{
    /// <summary>
    /// Just counts words frequency
    /// </summary>
    public class SimpleAnalyzer : ITextAnalyzer
    {
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

            return countedWords.Select(w =>
            {
                w.Value.Frequency = (double)w.Value.Count / wordsCounter;
                return w.Value;
            }).OrderByDescending(w => w.Count).Take(count).ToArray();
        }
    }
}