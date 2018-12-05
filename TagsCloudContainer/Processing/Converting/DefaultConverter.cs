using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Processing.Converting
{
    public class DefaultConverter : IWordConverter
    {
        public IEnumerable<string> Convert(IEnumerable<string> words) => words.Select(word => word.ToLower());
    }
}