using System.Collections.Generic;
using TagCloud.Infrastructure;

namespace TagCloud.WordsPreparation
{
    public interface IWordCountSetter
    {
        IEnumerable<Word> GetCountedWords(IEnumerable<Word> words);
    }
}
