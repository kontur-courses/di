using System.Collections.Generic;

namespace TagsCloudVisualization.WordCounter
{
    public interface IWordCounter
    {
        IEnumerable<WordCount> Count(IEnumerable<string> words);
    }
}