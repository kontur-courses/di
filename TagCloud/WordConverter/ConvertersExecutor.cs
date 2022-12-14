using System.Collections.Generic;
using System.Linq;

namespace TagCloud.WordConverter
{
    public class ConvertersExecutor : IConvertersExecutor
    {
        private readonly IEnumerable<IWordConverter> Converters;

        public ConvertersExecutor(IEnumerable<IWordConverter> converters)
        {
            Converters = converters;
        }

        public IReadOnlyList<string> Convert(IEnumerable<string> words)
        {
            return words.Select(word => Converters.Aggregate(word, (current, converter) => converter.Convert(current)))
                        .ToList();
        }
    }
}
