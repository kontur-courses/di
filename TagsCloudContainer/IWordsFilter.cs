using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IWordsFilter
    {
        public IEnumerable<string> Filter(IEnumerable<string> words);
    }
}