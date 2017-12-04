using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface IReader
    {
        IEnumerable<string> ReadWords();
    }
}