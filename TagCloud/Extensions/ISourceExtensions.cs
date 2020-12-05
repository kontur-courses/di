using System.Collections.Generic;
using TagCloud.Sources;

namespace TagCloud.Extensions
{
    public static class ISourceExtensions
    {
        public static Dictionary<string, double> GetWordsWithWeight(this ISource source)
        {
            var words = new Dictionary<string, double>();
            foreach (var word in source.Words())
                if (words.TryGetValue(word, out var value))
                    words[word] = value + 1;
                else
                    words[word] = 1;

            return words;
        }
    }
}
