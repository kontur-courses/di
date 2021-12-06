using System.Collections.Generic;
using System.Linq;

namespace TagCloud.Words.Preprocessing
{
    public class ToLowerCasePreprocessor : IPreprocessor
    {
        public IEnumerable<string> Preprocess(IEnumerable<string> words)
        {
            return words.Select(word => word.ToLower());
        }
    }
}