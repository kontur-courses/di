using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MyStemWrapper;

namespace TagsCloudVisualisation.Text.Preprocessing
{
    public class MyStemWordsConverter : IWordConverter
    {
        private const string ExeName = @"mystem.exe";

        private readonly Lazy<Lemmatizer> normalizer;

        public MyStemWordsConverter()
        {
            normalizer = new Lazy<Lemmatizer>(() =>
            {
                if (!File.Exists(ExeName))
                    throw new FileNotFoundException($"Missed mystem library on path {Path.GetFullPath(ExeName)}");
                return new Lemmatizer();
            });
        }

        public IEnumerable<string> Normalize(IEnumerable<string> words) =>
            normalizer.Value.GetWords(string.Join(" ", words))
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => x.TrimEnd('?'))
                .ToArray();
    }
}