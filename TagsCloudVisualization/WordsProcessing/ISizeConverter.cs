using System.Collections.Generic;

namespace TagsCloudVisualization.WordsProcessing
{
    public interface ISizeConverter
    {
        IEnumerable<SizedWord> Convert();
    }
}