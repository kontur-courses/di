using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization;

namespace TagsCloudContainer
{
    public class TagsCloudGenerator
    {
        private readonly Size minLetterSize;

        public TagsCloudGenerator(Size minLetterSize)
        {
            this.minLetterSize = minLetterSize;
        }

        public ITagsCloud CreateCloud(List<string> words,
            IWordsFilter wordsFilter,
            IWordsFormatter wordsFormatter,
            ITagsCloudLayouter layouter)
        {
            words = words.Select(wordsFormatter.Format).Where(wordsFilter.Filter).ToList();
            var frequencies = GetWordsFrequencies(words);
            var wordsSizes = GetWordsSizes(frequencies);

            foreach (var pair in wordsSizes)
            {
                layouter.PutNextWord(pair.Key, pair.Value);
            }

            return layouter.TagsCloud;
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

        private Dictionary<string, Size> GetWordsSizes(Dictionary<string, int> frequencies)
        {
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
    }
}