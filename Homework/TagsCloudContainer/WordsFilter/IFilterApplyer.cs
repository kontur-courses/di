using System.Collections.Generic;
using TagsCloudContainer.WordsPreparator;

namespace TagsCloudContainer.WordsFilter
{
    public interface IFilterApplyer
    {
        public ICollection<string> Apply(ICollection<WordInfo> words);
    }
}