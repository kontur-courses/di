using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace TagCloud.WordsPreprocessing.DocumentParsers
{
    public interface IDocumentParser
    {
        IEnumerable<string> GetWords(StreamReader stream);
    }
}