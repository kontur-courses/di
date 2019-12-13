using System.Collections.Generic;

namespace TagsCloudApp.Reader
{
    public interface IFileReader
    {
        IEnumerable<string> ReadWords(string filename);
    }
}
