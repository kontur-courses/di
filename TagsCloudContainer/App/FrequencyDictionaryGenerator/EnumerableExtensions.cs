using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Infrastructure.DictionaryGenerator;

namespace TagsCloudContainer.App.FrequencyDictionaryGenerator
{
    internal static class EnumerableExtensions
    {
        public static IEnumerable<string> FilterOutBoringWords(this IEnumerable<string> words, 
            IEnumerable<IWordFilter> filters)
        {
            foreach (var filter in filters)
                words = words.Where(word => !filter.IsBoring(word));
            return words;
        }

        public static IEnumerable<string> NormalizeWords(this IEnumerable<string> words, 
            IEnumerable<IWordNormalizer> normalizers)
        {
            foreach (var normalizer in normalizers)
                words = words.Select(word => normalizer.NormalizeWord(word));
            return words;
        }
    }
}