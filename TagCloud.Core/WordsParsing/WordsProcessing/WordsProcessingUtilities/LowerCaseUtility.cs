using System.Collections.Generic;
using System.Linq;

namespace TagCloud.Core.WordsParsing.WordsProcessing.WordsProcessingUtilities
{
    public class LowerCaseUtility : IWordsProcessingUtility
    {
        public IEnumerable<string> Process(IEnumerable<string> words)
        {
            return words.Select(word => word.ToLower());
        }
    }
}