using System.Collections.Generic;

namespace TagsCloudPreprocessor
{
    public interface IPreprocessor
    {
        IEnumerable<string> GetValidWords(string path, int count);
    }
}