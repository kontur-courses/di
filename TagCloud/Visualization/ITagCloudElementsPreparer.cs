using System.Collections.Generic;
using TagCloud.Infrastructure;

namespace TagCloud.Visualization
{
    public interface ITagCloudElementsPreparer
    {
        IEnumerable<TagCloudElement> PrepareTagCloudElements(IEnumerable<Word> words);
        int CurrentWordIndex { get; }
    }
}
