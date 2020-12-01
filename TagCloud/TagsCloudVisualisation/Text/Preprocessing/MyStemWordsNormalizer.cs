using System.Collections.Generic;
using System.Linq;
using MyStemWrapper;

namespace TagsCloudVisualisation.Text.Preprocessing
{
    public class MyStemWordsNormalizer : IWordNormalizer
    {
        //TODO enhance library source code: don't create new Process on each request to MyStem
        private readonly Lemmatizer normalizer;

        public MyStemWordsNormalizer()
        {
            normalizer = new Lemmatizer();
        }

        public IEnumerable<string> Normalize(IEnumerable<string> words)
        {
            return normalizer.GetWords(string.Join(" ", words)).Where(x => !string.IsNullOrEmpty(x)).ToArray();
        }
    }
}