using System.Collections.Generic;

namespace TagCloud.WordPreprocessors
{
    public interface IWordPreprocessor
    {
        public IEnumerable<string> GetPreprocessedWords();
    }
}
