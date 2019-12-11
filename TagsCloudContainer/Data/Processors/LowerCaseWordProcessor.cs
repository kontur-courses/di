using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Data.Processors
{
    public class LowerCaseWordProcessor : IWordProcessor
    {
        public IEnumerable<string> Process(IEnumerable<string> words)
        {
            return words.Select(word => word.ToLower());
        }
    }
}