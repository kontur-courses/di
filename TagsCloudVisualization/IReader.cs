using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface IReader
    {
        IEnumerable<string> ReadLines();
        IEnumerable<string> ReadWords();
    }
}