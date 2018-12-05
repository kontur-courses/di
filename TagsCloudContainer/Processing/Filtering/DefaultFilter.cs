using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Processing.Filtering
{
    public class DefaultFilter : IWordFilter
    {
        public IEnumerable<string> Filter(IEnumerable<string> words) =>
            words.Where(word => !string.IsNullOrEmpty(word));
    }
}