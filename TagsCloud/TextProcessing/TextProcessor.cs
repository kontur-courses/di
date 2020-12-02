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
        private readonly IFiltersApplier filtersApplier;
        private readonly IConvertersApplier convertersApplier;

        public TextProcessor(IReadersFactory readers, IFiltersApplier filters,
            IConvertersApplier converters)
        {
            readersFactory = readers;
            filtersApplier = filters;
            convertersApplier = converters;
        }

        public IEnumerable<WordInfo> ReadFromFile(string path)
        {
            var words = readersFactory.CreateReader().ReadWords(path);
            var filteredWords = filtersApplier.ApplyFilters(words);
            var convertedWords = convertersApplier.ApplyConversion(filteredWords);

            return convertedWords.GroupBy(w => w).Select(g => new WordInfo(g.Key, g.Count()));
        }
    }
}
