using DeepMorphy.Model;
using System.Collections.Generic;
using System.Linq;
using TagsCloud.Interfaces;

namespace TagsCloud.TextWorkers
{
    public class MorphsFilter : IMorphsFilter
    {
        private readonly ITextPartsToExclude excludeParts;

        public MorphsFilter(ITextPartsToExclude excludeParts)
        {
            this.excludeParts = excludeParts;
        }

        public IEnumerable<MorphInfo> FilterRedundantWords(IEnumerable<MorphInfo> morphs)
        {
            var clearMorphs = morphs
                .Where(x => !excludeParts.SpeechPartsToExclude.Contains(x.BestTag.GramsDic["чр"]));

            return clearMorphs;
        }
    }
}
