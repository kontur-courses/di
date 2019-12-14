using System.Collections.Generic;
using TextPreprocessor.Core;

namespace TextPreprocessor.TextAnalyzers
{
    public interface ITextAnalyzer
    {
        IEnumerable<TagInfo> GetTagInfo(IEnumerable<Tag> tags);
    }
}