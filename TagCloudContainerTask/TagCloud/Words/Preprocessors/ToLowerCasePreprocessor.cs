using System.Collections.Generic;
using System.Linq;

namespace TagCloud.Words.Preprocessors
{
    public class ToLowerCasePreprocessor : IPreprocessor
    {
        public IEnumerable<string> Preprocess(IEnumerable<string> words)
        {
            return words.Select(word => word.ToLower());
        }
    }
}