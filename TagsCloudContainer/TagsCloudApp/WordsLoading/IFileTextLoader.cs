using System.Collections.Generic;

namespace TagsCloudApp.WordsLoading
{
    public interface IFileTextLoader
    {
        IEnumerable<FileType> SupportedTypes { get; }
        string LoadText(string filename);
    }
}