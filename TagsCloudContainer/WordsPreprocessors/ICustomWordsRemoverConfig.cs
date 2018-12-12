using System.Collections.Generic;

namespace TagsCloudContainer.WordsPreprocessors
{
    public interface ICustomWordsRemoverConfig
    {
        IEnumerable<string> CustomBoringWords { get; }
    }
}