using System.Collections.Generic;

namespace TagsCloudVisualization.Preprocessing
{
    public interface IPreprocessAction
    {
        IEnumerable<Word> ProcessWords(IEnumerable<Word> words);
    }
}