using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization.FileReaders
{
    public class TxtFileReader : IFileReader
    {
        public string FilePath { get; }

        public TxtFileReader(string filePath)
        {
            FilePath = filePath;
        }

        public string ReadAllText()
        {
            if (!File.Exists(FilePath))
                throw new FileNotFoundException($"File {FilePath } doesn't exist");

            var text = File.ReadAllText(FilePath);

            if (text.Length == 0)
                throw new Exception($"File {FilePath } is empty");

            return text;
        }
    }
}
