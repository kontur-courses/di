using System.Collections.Generic;
using TagCloud.Infrastructure;

namespace TagCloud.WordsProcessing
{
    public interface IWordCounter
    {
        IEnumerable<Word> GetCountedWords(IEnumerable<Word> words);
    }
}
