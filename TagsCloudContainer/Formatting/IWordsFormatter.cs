using System.Collections.Generic;

namespace TagsCloudContainer.Formatters
{
    public interface IWordsFormatter
    {
        List<string> Format(List<string> words);
    }
}