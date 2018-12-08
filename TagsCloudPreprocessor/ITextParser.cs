using System.Collections.Generic;

namespace TagsCloudPreprocessor
{
    public interface ITextParser
    {
        IEnumerable<string> GetWords(string text);
    }
}
