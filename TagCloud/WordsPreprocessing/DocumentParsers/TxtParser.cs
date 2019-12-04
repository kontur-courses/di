using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud.WordsPreprocessing.DocumentParsers
{
    /// <summary>
    /// Returns whole text as one string from the StreamReader
    /// </summary>
    class TxtParser : IDocumentParser
    {
        public IEnumerable<string> GetWords(StreamReader stream)
        {
            yield return stream.ReadToEnd();
        }
    }
}
