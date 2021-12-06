using System.Collections.Generic;

namespace TagCloud.Words.Preprocessing
{
    public interface IPreprocessor
    {
        IEnumerable<string> Preprocess(IEnumerable<string> words);
    }
}