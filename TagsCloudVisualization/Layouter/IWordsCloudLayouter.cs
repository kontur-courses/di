using System.Collections.Generic;

namespace TagsCloudVisualization.Layouter
{
    public interface IWordsCloudLayouter
    {
        IEnumerable<Word> LayWords();
    }
}