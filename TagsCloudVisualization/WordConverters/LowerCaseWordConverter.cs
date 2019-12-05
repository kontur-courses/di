using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordConverters
{
    public class LowerCaseWordConverter : IWordConverter
    {
        public IEnumerable<string> ConvertWords(IEnumerable<string> words)
        {
            return words.Select(word => word.ToLower());
        }
    }
}