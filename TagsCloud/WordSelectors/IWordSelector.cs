using System.Collections.Generic;

namespace TagsCloud.WordSelector
{
    public interface IWordSelector
    {
        IEnumerable<string> TakeSelectedWords(IEnumerable<string> words);
    }
}