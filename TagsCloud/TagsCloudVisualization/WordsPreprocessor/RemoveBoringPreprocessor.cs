using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordsPreprocessor
{
    public class RemoveBoringPreprocessor : IWordsPreprocessor
    {
        private readonly HashSet<string> _boringWords;

        public RemoveBoringPreprocessor(IEnumerable<string> boringWords)
        {
            _boringWords = boringWords.ToHashSet();
        }

        public IEnumerable<string> Process(IEnumerable<string> words) => words.Where(w => !IsBoring(w));

        private bool IsBoring(string word) => _boringWords.Contains(word);
    }
}