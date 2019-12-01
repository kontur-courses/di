using System;
using System.Collections.Generic;
using System.IO;
using TagsCloudLibrary.MyStem;

namespace TagsCloudLibrary.WordsExtractor
{
    public class LiteratureExtractor : IWordsExtractor
    {
        public IEnumerable<string> ExtractWords(Stream stream)
        {
            var myStem = new MyStemProcess();
            return myStem.StreamToWords(stream);
        }
    }
}