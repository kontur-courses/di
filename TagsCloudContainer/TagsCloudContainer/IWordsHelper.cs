using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IWordsHelper
    {
        List<string> GetAllWordsToVisualize(List<string> words);
    }
}