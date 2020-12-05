using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IFileReader
    {
        public string Format { get; set; }
        public IEnumerable<string> ReadAllLines(string filePath);
    }
}