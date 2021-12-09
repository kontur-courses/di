using System.Collections.Generic;

namespace TagCloud.Words.Preprocessors
{
    public interface IPreprocessor
    {
        IEnumerable<string> Preprocess(IEnumerable<string> words);
    }
}