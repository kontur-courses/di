using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.WordConverter;
using TagsCloudContainer.WordFilter;

namespace TagsCloudContainer.Settings
{
    public class TextSettings
    {
        public TextSettings(int countWords, IEnumerable<string> filters, IEnumerable<string> wordConverters)
        {
            CountWords = countWords;
            WordConverters = GetConvertersByName(wordConverters);
            WordFilters = GetFiltersByName(filters);
        }

        public IFilter[] WordFilters { get; }
        public int CountWords { get; }
        public IWordConverter[] WordConverters { get; }


        private IWordConverter[] GetConvertersByName(IEnumerable<string> wordConverters)
        {
            return wordConverters.Select(converter => Converters.WordConvertersDictionary[converter]).ToArray();
        }

        private IFilter[] GetFiltersByName(IEnumerable<string> filters)
        {
            return filters.Select(filter => Filters.FiltersDictionary[filter]).ToArray();
        }
    }
}