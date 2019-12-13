using System.Collections.Generic;

namespace TagsCloudContainer.Data.Readers
{
    public interface IFileFormatReader : IWordsFileReader
    {
        IEnumerable<string> Extensions { get; }
    }
}