using System.Collections.Generic;

namespace TagsCloudVisualization.WordsPrepares
{
    public interface IWordsPreparer
    {
        IEnumerable<string> Prepare(IEnumerable<string> words);
    }
}