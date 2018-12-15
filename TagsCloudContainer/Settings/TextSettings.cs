using System.Collections.Generic;
using TagsCloudContainer.WordConverter;
using TagsCloudContainer.WordFilter;

namespace TagsCloudContainer.Settings
{
    public class TextSettings
    {
        public TextSettings(int countWords, IEnumerable<string> filters, IEnumerable<string> wordConverters, FilterSettings filterSettings)
        {
            CountWords = countWords;
            WordConverters = Converters.GetConvertersByName(wordConverters);
            WordFilters = new Filters(filterSettings).GetFiltersByName(filters);
        }

        public IFilter[] WordFilters { get; }
        public int CountWords { get; }
        public IWordConverter[] WordConverters { get; }
    }
}