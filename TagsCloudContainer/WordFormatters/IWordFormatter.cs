using System.Collections.Generic;

namespace TagsCloudContainer.WordFormatters
{
    public interface IWordFormatter
    {
        IEnumerable<Word> FormatWords(IEnumerable<string> words);
    }
}