using System.Collections.Generic;

namespace TagCloud.TextAnalyzer
{
    public interface ITextAnalyzer
    {
        public HashSet<TagInfo> GetTags(List<string> words);
    }
}