using System;
using System.Collections.Generic;
using TagCloudCreation;

namespace TagCloudApp
{
    internal class SelectedBoringWordsRemover : IWordPreparer
    {
        private static string path;
        private static ITextReader textReader;
        private readonly Lazy<HashSet<string>> boringWords = new Lazy<HashSet<string>>(ReadWords);

        public SelectedBoringWordsRemover(ITextReader reader)
        {
            textReader = reader;
        }

        public string PrepareWord(string word, TagCloudCreationOptions options)
        {
            if (options.PathToBoringWords == null)
                return word;
            if (!boringWords.IsValueCreated)
                path = options.PathToBoringWords;
            return boringWords.Value.Contains(word) ? null : word;
        }

        private static HashSet<string> ReadWords()
        {
            textReader.TryReadWords(path, out var words);
            return new HashSet<string>(words);
        }
    }
}
