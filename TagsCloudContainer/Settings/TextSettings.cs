using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagsCloudContainer.WordConverter;
using TagsCloudContainer.WordFilter;

namespace TagsCloudContainer.Settings
{
    public class TextSettings
    {
        public TextSettings(int countWords, IEnumerable<string> filters, IEnumerable<string> wordConverters, string boringWordFileName = null)
        {
            CountWords = countWords;
            BoringWordFileName = boringWordFileName;
            WordConverters = GetConvertersByName(wordConverters);
            WordFilters = GetFiltersByName(filters);
        }

        public IFilter[] WordFilters { get; }
        public int CountWords { get; }
        public IWordConverter[] WordConverters { get; }
        private string BoringWordFileName { get; }


        private IWordConverter[] GetConvertersByName(IEnumerable<string> wordConverters)
        {
            return wordConverters.Select(converter => Converters.WordConvertersDictionary[converter]).ToArray();
        }

        private IFilter[] GetFiltersByName(IEnumerable<string> filters)
        {
            var resultFilters = new List<IFilter>();
            foreach (var filter in filters)
            {
                if (filter == "boring")
                {
                    var boringWords = File.ReadLines(BoringWordFileName).ToList();
                    resultFilters.Add(new BoringWordFilter(boringWords));
                }
                else 
                    resultFilters.Add(Filters.FiltersDictionary[filter]);
            }
            return resultFilters.ToArray();
        }
    }
}