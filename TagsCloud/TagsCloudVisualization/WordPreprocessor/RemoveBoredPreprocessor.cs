using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordPreprocessor
{
    public class RemoveBoredPreprocessor : IWordsPreprocessor
    {
        public IEnumerable<string> Process(IEnumerable<string> words) => words.Where(w => !IsBored(w));

        private static bool IsBored(string word) => throw new NotImplementedException();
    }
}