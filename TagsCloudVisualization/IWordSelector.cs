using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface IWordSelector
    {
        IEnumerable<string> GetWords(string text);
    }
}