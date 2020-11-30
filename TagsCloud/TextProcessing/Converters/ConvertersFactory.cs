using System.Collections.Generic;
using System.Linq;

namespace TagsCloud.TextProcessing.Converters
{
    public class ConvertersFactory : IConvertersFactory
    {
        private readonly Dictionary<string, IWordConverter> wordConverters;

        public ConvertersFactory(IEnumerable<IWordConverter> wordConverters)
        {
            this.wordConverters = wordConverters.ToDictionary(convert => convert.Name);
        }

        public IEnumerable<string> ApplyConversion(IEnumerable<string> words, string[] converterNames)
        {
            return words.Select(word => converterNames
                                        .Select(converterName => wordConverters[converterName])
                                        .Aggregate(word, (current, converter) => converter.Convert(current)));
        }

        public IEnumerable<string> GetConverterNames() => wordConverters.Select(pair => pair.Key);
    }
}
