using System.Collections.Generic;
using TagsCloudContainer.WordsConverters;

namespace TagsCloudContainer.WordsFilter
{
    public interface IFilterApplyer
    {
        public ICollection<string> Apply(ICollection<WordInfo> words);
    }
}