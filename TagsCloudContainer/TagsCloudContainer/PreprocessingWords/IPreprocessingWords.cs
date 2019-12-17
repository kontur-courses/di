using System.Collections.Generic;

namespace TagsCloudContainer.PreprocessingWords
{
    public interface IPreprocessingWords
    {
        IEnumerable<string> Preprocessing(IEnumerable<string> strings);
    }
}