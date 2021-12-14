using System.Collections.Generic;

namespace TagsCloudVisualization.Common.FileReaders
{
    public interface IFileReader
    {
        public string ReadFile(string path);
        
        public IEnumerable<string> ReadLines(string path);
    }
}