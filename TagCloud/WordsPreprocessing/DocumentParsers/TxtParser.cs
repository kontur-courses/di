using System.Collections.Generic;
using TagCloud.Interfaces;

namespace TagCloud.WordsPreprocessing.DocumentParsers
{
    /// <summary>
    /// Returns whole text as one string from the StreamReader
    /// </summary>
    class TxtParser : IDocumentParser
    {
        public HashSet<string> AllowedTypes => new HashSet<string>{".txt"};


        public IEnumerable<string> GetWords(ApplicationSettings settings)
        {
            
            return settings.GetDocumentStream().ReadToEnd().Split(' ');
        }
    }
}
