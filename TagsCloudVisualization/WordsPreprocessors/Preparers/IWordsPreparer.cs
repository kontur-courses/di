using System.Collections.Generic;

namespace TagsCloudVisualization.WordsPreprocessors.Preparers
{
    public interface IWordsPreparer
    {
        IEnumerable<string> Prepare(IEnumerable<string> words);
    }
}