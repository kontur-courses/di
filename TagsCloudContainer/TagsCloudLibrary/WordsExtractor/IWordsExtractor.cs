using System.Collections.Generic;
using System.IO;

namespace TagsCloudLibrary.WordsExtractor
{
    public interface IWordsExtractor
    {
        IEnumerable<string> ExtractWords(Stream stream);
    }
}
