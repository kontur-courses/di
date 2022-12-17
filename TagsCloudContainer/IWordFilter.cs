using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IWordFilter
    {
        HashSet<string> WordsToFilter { get; set; }
    }
}