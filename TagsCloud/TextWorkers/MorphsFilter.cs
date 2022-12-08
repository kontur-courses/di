using DeepMorphy.Model;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloud.TextWorkers
{
    public class MorphsFilter
    {
        private readonly string[] speechPartsToExclude =
        {
            "мест",
            "межд",
            "част",
            "предл",
            "союз"
        };

        public IEnumerable<MorphInfo> FilterRedutantWords(IEnumerable<MorphInfo> morphs)
        {
            var clearMorphs = morphs
                .Where(x => !speechPartsToExclude.Contains(x.BestTag.GramsDic["чр"]));

            return clearMorphs;
        }
    }
}
