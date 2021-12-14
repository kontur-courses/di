using System.Collections.Generic;

namespace TagsCloudVisualization.Common.WordFilters
{
    public interface IWordFilter
    {
        public IEnumerable<string> Filter(IEnumerable<string> words);
    }
}