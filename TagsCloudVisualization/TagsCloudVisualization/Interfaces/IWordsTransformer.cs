using System.Collections.Generic;

namespace TagsCloudVisualization.Interfaces
{
    public interface IWordsTransformer
    {
        List<string> GetStems(List<string> words);
    }
}