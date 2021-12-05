using System.Collections.Generic;

namespace TagsCloudVisualization.TextAnalization.NormalizationMaker
{
    public interface INormalizationMaker
    {
        IEnumerable<Word> MakeNormalization(IEnumerable<Word> word);
    }
}
