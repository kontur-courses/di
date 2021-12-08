using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordsPreprocessor
{
    public class ToLowerCasePreprocessor : IWordsPreprocessor
    {
        public IEnumerable<string> Process(IEnumerable<string> words) => words.Select(word => word.ToLowerInvariant());
    }
}