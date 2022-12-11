using System.Collections.Generic;

namespace TagCloud.Readers
{
    public interface IReader
    {
        public string FileExtFilter { get; }
        public void SetFile(string path);
        public IEnumerable<string> ReadWords();
    }
}
