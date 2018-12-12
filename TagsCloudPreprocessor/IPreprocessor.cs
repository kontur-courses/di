using System.Collections.Generic;

namespace TagsCloudPreprocessor
{
    public interface IPreprocessor
    {
        IEnumerable<(string Word, int CountInText)> GetValidWordsWithCount(string path, int count);
    }
}