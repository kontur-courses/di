using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordsPreprocessor
{
    public class RemoveBoredPreprocessor : IWordsPreprocessor
    {
        private readonly HashSet<string> _boredWords;

        public RemoveBoredPreprocessor(IEnumerable<string> boredWords)
        {
            _boredWords = boredWords.ToHashSet();
        }
        public IEnumerable<string> Process(IEnumerable<string> words) => words.Where(w => !IsBored(w));

        private bool IsBored(string word) => _boredWords.Contains(word);
    }
}