using System.Collections.Generic;
using System.Linq;
using TagCloudContainer.Api;

namespace TagCloudContainer.Implementations
{
    [CliElement("lowercase", typeof(LowercaseWordProcessor))]
    public class LowercaseWordProcessor : IWordProcessor
    {
        public IEnumerable<string> Process(IEnumerable<string> words)
        {
            return words.Where(word => word.Length > 2).Select(word => word.ToLower());
        }
    }
}