using System.Collections.Generic;
using System.IO;

namespace TagCloud.IReaders
{
    public class SingleWordInRowTextFileReader : IReader
    {
        private readonly string path;
        public SingleWordInRowTextFileReader(string path)
        {
            this.path = path;
            CheckFile();
        }

        public IEnumerable<string> ReadWords() =>
            File.ReadAllLines(path);

        private void CheckFile()
        {
            if (Path.GetExtension(path)!.ToLower() != ".txt")
                throw new FileLoadException("txt file only");

            if (!File.Exists(path))
                throw new FileNotFoundException($"file {path} not found");
        }
    }
}
