using System.Collections.Generic;
using System.Linq;

namespace TagCloud.WordConverter
{
    public class ConvertersExecutor : IConvertersExecutor
    {
        private readonly List<IWordConverter> Converters = new List<IWordConverter>();

        public ConvertersExecutor(IWordConverter[] converters)
        {
            foreach (var converter in converters)
                RegisterConverter(converter);
        }

        public void RegisterConverter(IWordConverter converter)
        {
            Converters.Add(converter);
        }

        public IReadOnlyList<string> Convert(IEnumerable<string> words)
        {
            return words
                       .Select(word => Converters.Aggregate(word, (current, converter) => converter.Convert(current)))
                       .ToList();
        }
    }
}
