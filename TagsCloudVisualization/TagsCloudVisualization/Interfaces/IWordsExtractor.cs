using System.Collections.Generic;

namespace TagsCloudVisualization.Interfaces
{
    public interface IWordsExtractor
    {
        List<string> GetWords(string path);
    }
}
