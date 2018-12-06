using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface IWordsCloudLayouter
    {
        IEnumerable<Word> LayWords(IEnumerable<SizedWord> sizedWords);
    }
}