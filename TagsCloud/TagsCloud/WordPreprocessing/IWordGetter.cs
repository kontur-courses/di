using System.Collections.Generic;

namespace TagsCloud.WordPreprocessing
{
    public interface IWordGetter
    {
        IEnumerable<string> GetWords(params char[] delimiters);
    }
}