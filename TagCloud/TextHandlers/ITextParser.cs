using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface ITextParser
    {
        IEnumerable<string> GetWords();
    }
}