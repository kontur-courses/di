using System.Collections.Generic;
using System.Linq;

namespace TagCloud.selectors
{
    public class WordConverter : IConverter<IEnumerable<string>>
    {
        private readonly List<IConverter<string>> singleConverters;

        public WordConverter(List<IConverter<string>> singleConverters)
        {
            this.singleConverters = singleConverters;
        }

        public IEnumerable<string> Convert(IEnumerable<string> source) =>
            source
                .Select(word =>
                    singleConverters.Aggregate(word, (current, converter) => converter.Convert(current))
                );
    }
}