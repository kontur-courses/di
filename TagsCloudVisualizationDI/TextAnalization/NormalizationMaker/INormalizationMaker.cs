using System.Collections.Generic;

namespace TagsCloudVisualizationDI.TextAnalization.NormalizationMaker
{
    public interface INormalizationMaker
    {
        IEnumerable<Word> MakeNormalization(IEnumerable<Word> word);
    }
}
