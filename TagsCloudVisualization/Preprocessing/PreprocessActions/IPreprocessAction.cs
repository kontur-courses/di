using System.Collections.Generic;
using TagsCloudVisualization.Core;

namespace TagsCloudVisualization.Preprocessing
{
    public interface IPreprocessAction
    {
        IEnumerable<Word> ProcessWords(IEnumerable<Word> words);
    }
}