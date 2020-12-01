using System.Collections.Generic;
using System.Linq;
using TagsCloud.TextProcessing.Converters;
using TagsCloud.TextProcessing.TextFilters;
using TagsCloud.TextProcessing.TextReaders;

namespace TagsCloud.TextProcessing
{
    public class TextProcessor
    {
        private readonly IReadersFactory readersFactory;
        private readonly IFiltersApplier filtersFactory;
        private readonly IConvertersApplier convertersFactory;

        public TextProcessor(IReadersFactory readers, IFiltersApplier filters,
            IConvertersApplier converters)
        {
            readersFactory = readers;
            filtersFactory = filters;
            convertersFactory = converters;
        }

        public IEnumerable<WordInfo> ReadFromFile(string path)
        {
            var reader = readersFactory.CreateReader(path);
            var words = reader.ReadWords(path);
            var filtredWords = filtersFactory.ApplyFilters(words);
            var convertedWords = convertersFactory.ApplyConversion(filtredWords);

            return convertedWords.GroupBy(w => w).Select(g => new WordInfo(g.Key, g.Count()));
        }
    }
}
