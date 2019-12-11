using System.Collections.Generic;

namespace TextConfiguration
{
    public interface ITextPreprocessor
    {
        List<string> PreprocessText(string text);
    }
}
