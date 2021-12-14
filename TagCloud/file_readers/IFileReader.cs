using System.Collections.Generic;

namespace TagCloud.file_readers
{
    public interface IFileReader
    {
        List<string> GetWords(string filename);
    }
}