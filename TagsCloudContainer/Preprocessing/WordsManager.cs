using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloudVisualization;

namespace TagsCloudContainer
{
    public class WordsManager
    {
        private readonly Dictionary<string, int> wordsFrequencies = new Dictionary<string, int>();

        public void AddWord(string word)
        {
            wordsFrequencies.TryGetValue(word, out var value);
            wordsFrequencies[word] = value + 1;
        }

        public IEnumerable<(string, int)> GetOrderedWordsAndFrequencies()
        {
            return wordsFrequencies
                .OrderBy(kv => kv.Value)
                .Select(kv => (word: kv.Key, frequency: kv.Value));
        }
    }
}