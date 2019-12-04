using System.Collections.Generic;
using TagsCloudContainer.WordProcessing.Converting;
using TagsCloudContainer.WordProcessing.Filtering;

namespace TagsCloudContainer.WordProcessing
{
    public class WordProcessor : IWordProcessor
    {
        private readonly IWordConverter wordConverter;
        private readonly IWordFilter wordFilter;

        public WordProcessor(IWordConverter wordConverter, IWordFilter wordFilter)
        {
            this.wordConverter = wordConverter;
            this.wordFilter = wordFilter;
        }

        public IEnumerable<string> ProcessWords(IEnumerable<string> words)
        {
            var convertedWords = wordConverter.ConvertWords(words);
            return wordFilter.FilterWords(convertedWords);
        }
    }
}