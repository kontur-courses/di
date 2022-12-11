using DeepMorphy.Model;
using System.Collections.Generic;
using System.Linq;
using TagsCloud.Interfaces;

namespace TagsCloud.TextWorkers
{
    public class NormalFormParser : INormalFormParser
    {
        public IEnumerable<string> Normalize(IEnumerable<MorphInfo> clearMorphs)
        {
            var normalForms = clearMorphs.Select(x => x.BestTag.Lemma);

            return normalForms;
        }
    }
}
