using System.Collections.Generic;
using System.Linq;

namespace TagsCloudPreprocessor
{
    public class PreprocessorLinear:IPreprocessor
    {
        public IEnumerable<string> GetValidWords(IEnumerable<string> words)
        {
            var validWords = words
                .OrderBy(w => w)
                .Reverse();

            return validWords;
        }
    }
}
