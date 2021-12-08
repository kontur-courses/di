using System.Collections.Generic;

namespace TagsCloudContainer.FileReader
{
    public interface IFileReader
    {
        public IEnumerable<string> ReadWords(string path);
    }
}