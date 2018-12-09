using System.Collections.Generic;

namespace TagsCloudVisualization.Layouter
{
    public interface IWordsCloudBuilder
    {
        IEnumerable<Word> Build();
    }
}