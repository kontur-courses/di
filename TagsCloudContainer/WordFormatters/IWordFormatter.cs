using System.Collections.Generic;

namespace TagsCloudContainer.WordFormatters
{
    public interface IWordFormatter
    {
        IEnumerable<IFormattedWord> FormatWords(IEnumerable<string> words);
    }
}