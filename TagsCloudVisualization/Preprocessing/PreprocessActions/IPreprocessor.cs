using System.Collections.Generic;

namespace TagsCloudVisualization.Preprocessing
{
    public interface IPreprocessor
    {
        IEnumerable<string> ProcessWords(IEnumerable<string> words);
    }
}