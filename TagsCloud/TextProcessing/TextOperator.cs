using System.Collections.Generic;
using System.Linq;
using TagsCloud.TextProcessing.Converters;
using TagsCloud.TextProcessing.TextFilters;
using TagsCloud.TextProcessing.TextReaders;
using TagsCloud.TextProcessing.WordConfig;

namespace TagsCloud.TextProcessing
{
    public class TextOperator
    {
        private readonly IReadersFactory readersFactory;
        private readonly IFiltersFactory filtersFactory;
        private readonly IConvertersFactory convertersFactory;
        private readonly IWordsConfig wordsConfig;

        public TextOperator(IReadersFactory readers, IFiltersFactory filters,
            IConvertersFactory converters, IWordsConfig wordsConfig)
        {
            readersFactory = readers;
            filtersFactory = filters;
            convertersFactory = converters;
            this.wordsConfig = wordsConfig;
        }

        public IEnumerable<WordInfo> ReadFromFile(string path)
        {
            var words = readersFactory.ReadText(path);
            var filtredWords = filtersFactory.ApplyFilters(words, wordsConfig.FilerNames);
            var convertedWords = convertersFactory.ApplyConversion(filtredWords, wordsConfig.ConvertersNames);

            return convertedWords.GroupBy(w => w).Select(g => new WordInfo(g.Key, g.Count()));
        }
    }
}
