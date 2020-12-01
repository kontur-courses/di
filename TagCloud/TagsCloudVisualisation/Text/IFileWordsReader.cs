using System.Collections.Generic;

namespace TagsCloudVisualisation.Text
{
    public interface IFileWordsReader
    {
        IEnumerable<string> EnumerateWordsFrom(string path);
    }
}