using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface IWordProvider
    {
        List<string> GetWords(string filepath);
    }
}