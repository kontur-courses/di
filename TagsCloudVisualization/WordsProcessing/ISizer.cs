using System.Collections.Generic;

namespace TagsCloudVisualization.WordsProcessing
{
    public interface ISizer
    {
        IEnumerable<SizedWord> SizeWords();
    }
}