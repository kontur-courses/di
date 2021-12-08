using System.Collections.Generic;

namespace TagsCloudContainer.TextPreparation
{
    public interface IWordsHelper
    {
        List<string> GetAllWordsToVisualize(List<string> words);
    }
}