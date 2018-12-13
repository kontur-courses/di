using System.Collections.Generic;

namespace TagsCloudPreprocessor.Preprocessors
{
    public interface IPreprocessor
    {
        List<string> PreprocessWords(List<string> words);
    }
}