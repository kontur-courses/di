using System.Collections.Generic;

namespace TagsCloudVisualization.WordsPrepare
{
    public interface IWordsPreparer
    {
        IEnumerable<string> Prepare(IEnumerable<string> words);
    }
}