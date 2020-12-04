using System.Collections.Generic;
using System.IO;
using System.Linq;
using MyStemWrapper;

namespace TagsCloudVisualisation.Text.Preprocessing
{
    public class MyStemWordsNormalizer : IWordNormalizer
    {
        private const string ExeName = @"mystem.exe";

        private readonly Lemmatizer normalizer;

        public MyStemWordsNormalizer()
        {
            if (!File.Exists(ExeName))
                throw new FileNotFoundException($"Missed mystem library on path {Path.GetFullPath(ExeName)}");
            normalizer = new Lemmatizer();
        }

        public IEnumerable<string> Normalize(IEnumerable<string> words) =>
            normalizer.GetWords(string.Join(" ", words))
                .Where(x => !string.IsNullOrEmpty(x))
                .ToArray();
    }
}