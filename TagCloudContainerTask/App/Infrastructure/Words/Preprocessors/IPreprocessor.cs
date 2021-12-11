using System.Collections.Generic;

namespace App.Infrastructure.Words.Preprocessors
{
    public interface IPreprocessor
    {
        IEnumerable<string> Preprocess(IEnumerable<string> words);
    }
}