using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization.TextAnalization.NormalizationMaker
{
    public interface INormalizationMaker
    {
        IEnumerable<Word> MakeNormalization(IEnumerable<Word> word);
    }
}
