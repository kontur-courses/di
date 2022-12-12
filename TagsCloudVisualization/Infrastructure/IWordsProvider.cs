using System.Collections.Generic;

namespace TagsCloudVisualization.Infrastructure
{
    public interface IWordsProvider
    {
        IEnumerable<string> GetWords(string path);
    }
}