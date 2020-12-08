using System.Collections.Generic;

namespace TagsCloudContainer.Infrastructure.TextAnalyzer
{
    internal interface ITextParser
    {
        public IEnumerable<string> GetWords(IEnumerable<string> lines);
    }
}