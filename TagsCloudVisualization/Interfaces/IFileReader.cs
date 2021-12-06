using System.Collections.Generic;
using System.IO;

namespace TagsCloudVisualization.Interfaces
{
    public interface IFileReader
    {
        IEnumerable<string> GetWordsFromFile(StreamReader reader, char[] separators);
    }
}