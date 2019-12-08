using System.Collections.Generic;

namespace TagCloud
{
    public interface ITextAnalyzer
    {
        IEnumerable<TagInfo> GetWordsInfo();
    }
}