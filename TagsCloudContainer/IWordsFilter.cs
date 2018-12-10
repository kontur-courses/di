using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IWordsFilter
    {
        List<string> Filter(List<string> words);
    }
}