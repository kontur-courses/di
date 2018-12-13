using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Converter
{
    public class LowercaseConverter : IConverter
    {
        public IEnumerable<string> Convert(IEnumerable<string> words)
        {
            return words.Select(word => word.ToLower());
        }
    }
}