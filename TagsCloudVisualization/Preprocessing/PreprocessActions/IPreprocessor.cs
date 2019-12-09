using System.Collections.Generic;
using TagsCloudVisualization.Core;

namespace TagsCloudVisualization.Preprocessing
{
    public interface IPreprocessor
    {
        IEnumerable<string> ProcessWords(IEnumerable<string> words);
    }
}