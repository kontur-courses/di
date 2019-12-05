using System.Collections.Generic;

namespace TagsCloudContainer.Word_Counting
{
    public interface IWordCounter
    {
        Dictionary<string, int> CountWords(IEnumerable<string> words);
    }
}