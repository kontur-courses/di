using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.Preprocessing
{
    public class ToLowercasePreprocessor : IPreprocessor
    {
        public IEnumerable<string> ProcessWords(IEnumerable<string> words)
        {
            return words.Select(word => word.ToLower());
        }
    }
}