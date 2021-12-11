using System.Collections.Generic;

namespace TagsCloudContainer.FileReader
{
    public interface IFileReader
    {
        public string Extension { get; }
        public ICollection<string> ReadWords(string path);
    }
}