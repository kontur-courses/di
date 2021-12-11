using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Words.Preprocessors;

namespace App.Implementation.Words.Preprocessors
{
    public class ToLowerCasePreprocessor : IPreprocessor
    {
        public IEnumerable<string> Preprocess(IEnumerable<string> words)
        {
            return words.Select(word => word.ToLower());
        }
    }
}