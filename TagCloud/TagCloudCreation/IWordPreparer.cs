using System;
using System.Collections.Generic;
using System.IO;

namespace TagCloudCreation
{
    public interface IWordPreparer
    {
        /// <summary>
        ///     Transforms word
        /// </summary>
        /// <returns>null if this word should be removed</returns>
        WordInfo PrepareWords(WordInfo stat, TagCloudCreationOptions options);
    }

    public class SelectedBoringWordsRemover : IWordPreparer
    {
        private readonly Lazy<HashSet<string>> boringWords = new Lazy<HashSet<string>>(ReadWords);

        private static string path;

        public WordInfo PrepareWords(WordInfo stat, TagCloudCreationOptions options)
        {
            if (options.PathToBoringWords == null)
                return stat;
            if (!boringWords.IsValueCreated)
                path = options.PathToBoringWords;
            return boringWords.Value.Contains(stat.Word) ? null : stat;
        }

        private static HashSet<string> ReadWords()
        {
            var words = File.ReadAllLines(path);
            return new HashSet<string>(words);
        }
    }
}
