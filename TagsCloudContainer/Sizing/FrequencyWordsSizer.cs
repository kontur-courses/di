using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Sizing
{
    public class FrequencyWordsSizer : IWordsSizer
    {
        public Dictionary<string, Size> GetWordsSizes(List<string> words, Size minLetterSize)
        {
            var frequencies = GetWordsFrequencies(words);
            var wordsSizes = new Dictionary<string, Size>();
            foreach (var pair in frequencies)
            {
                var word = pair.Key;
                var frequency = pair.Value;
                wordsSizes[word] = new Size(minLetterSize.Width * frequency * word.Length,
                    minLetterSize.Height * frequency);
            }

            return wordsSizes;
        }

        private Dictionary<string, int> GetWordsFrequencies(List<string> words)
        {
            var frequencies = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (frequencies.ContainsKey(word))
                    frequencies[word]++;
                else
                {
                    frequencies[word] = 1;
                }
            }

            return frequencies;
        }
    }
}