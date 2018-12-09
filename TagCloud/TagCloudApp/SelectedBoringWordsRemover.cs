using System;
using System.Collections.Generic;
using TagCloudCreation;
using TagCloudVisualization;

namespace TagCloudApp
{
    public class SelectedBoringWordsRemover : IWordPreparer
    {
        private static string path;
        private static ITextReader textReader;
        private readonly Lazy<HashSet<string>> boringWords = new Lazy<HashSet<string>>(ReadWords);

        public SelectedBoringWordsRemover(ITextReader reader)
        {
            textReader = reader;
        }

        public WordInfo PrepareWord(WordInfo stat, TagCloudCreationOptions options)
        {
            if (options.PathToBoringWords == null)
                return stat;
            if (!boringWords.IsValueCreated)
                path = options.PathToBoringWords;
            return boringWords.Value.Contains(stat.Word) ? null : stat;
        }

        private static HashSet<string> ReadWords()
        {
            textReader.TryReadWords(path, out var words);
            return new HashSet<string>(words);
        }
    }
}
