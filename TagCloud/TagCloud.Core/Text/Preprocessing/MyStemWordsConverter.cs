using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MyStemWrapper;
using TagCloud.Core.Utils;

namespace TagCloud.Core.Text.Preprocessing
{
    public class MyStemWordsConverter : IWordConverter
    {
        private const string MyStemExe = @"mystem.exe";

        private readonly Lazy<Lemmatizer> normalizer;

        public MyStemWordsConverter()
        {
            normalizer = new Lazy<Lemmatizer>(() =>
            {
                if (!File.Exists(MyStemExe))
                    File.Copy(SharedBin.File(MyStemExe), MyStemExe);
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