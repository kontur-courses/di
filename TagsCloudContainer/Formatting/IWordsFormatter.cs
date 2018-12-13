using System.Collections.Generic;

namespace TagsCloudContainer.Formatting
{
    public interface IWordsFormatter
    {
        List<string> Format(IEnumerable<string> words);
    }
}