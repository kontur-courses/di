using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeCantSpell.Hunspell;

namespace TagsCloudVisualization.TextAnalization.NormalizationMaker
{
    public class NormalizationMaker : INormalizationMaker
    {
        public IEnumerable<Word> MakeNormalization(IEnumerable<Word> words)
        {
            //приводим слово к начальной форме
            foreach (var word in words)
            {
                yield return word;
            }
        }
    }
}
