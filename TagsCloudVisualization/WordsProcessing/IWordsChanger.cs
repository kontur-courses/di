using System.Collections.Generic;

namespace TagsCloudVisualization.WordsProcessing
{
    public interface IWordsChanger
    {
        IEnumerable<string> ChangeWords(IEnumerable<string> words);
    }
}