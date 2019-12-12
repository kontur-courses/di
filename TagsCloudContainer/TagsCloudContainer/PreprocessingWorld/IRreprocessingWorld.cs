using System.Collections.Generic;

namespace TagsCloudContainer.PreprocessingWorld
{
    public interface IPreprocessingWorld
    {
        IEnumerable<string> Preprocessing(IEnumerable<string> strings);
    }
}