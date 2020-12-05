using System.Collections.Generic;
using System.Linq;
using MyStemWrapper;

namespace TagCloud.Core.Text.Preprocessing
{
    public class MyStemWordsConverter : IWordConverter
    {
        private readonly Lemmatizer normalizer;

        public MyStemWordsConverter()
        {
            normalizer = new Lemmatizer();
        }

        public IEnumerable<string> Normalize(IEnumerable<string> words) =>
            normalizer.GetWords(string.Join(" ", words))
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => x.TrimEnd('?'))
                .ToArray();
    }
}