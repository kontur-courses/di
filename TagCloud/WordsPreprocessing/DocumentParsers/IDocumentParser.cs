using System.Collections.Generic;
using TagCloud.Interfaces;

namespace TagCloud.WordsPreprocessing.DocumentParsers
{
    public interface IDocumentParser
    {
        HashSet<string> AllowedTypes { get; }
        IEnumerable<string> GetWords(ApplicationSettings settings);
        void Close();
    }
}