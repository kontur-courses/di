using DeepMorphy.Model;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloud.TextWorker
{
    public static class NormalFormParser
    {
        public static IEnumerable<string> ConvertWordsToNormalForm(IEnumerable<MorphInfo> clearMorphs)
        {
            var normalForms = clearMorphs.Select(x => x.BestTag.Lemma);

            return normalForms;
        }
    }
}
