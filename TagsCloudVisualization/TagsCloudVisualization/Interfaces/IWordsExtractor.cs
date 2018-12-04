using System.Collections.Generic;

namespace TagsCloudVisualization.Interfaces
{
    public interface IWordsExtractor
    {
        List<string> Extract(string path);
    }
}
