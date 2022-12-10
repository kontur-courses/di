using System.Collections.Generic;
using TagCloud.Readers;

namespace TagCloud.WordPreprocessors
{
    public interface IWordPreprocessor
    {
        public IEnumerable<string> GetPreprocessedWords(IReader wordsReader);
    }
}
