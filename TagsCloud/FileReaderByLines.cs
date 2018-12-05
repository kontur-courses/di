using System.Collections.Generic;
using System.IO;
using System.Text;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    public class FileReaderByLines : IFileReader
    {
        public FileReaderByLines(string path)
        {
            Path = path;
        }

        public string Path { get; set; }

        public IEnumerable<string> Read()
        {
            var result = new List<string>();
            using (var sr = new StreamReader(Path, Encoding.Default))
            {
                while (!sr.EndOfStream) result.Add(sr.ReadLine());
            }

            return result;
        }
    }
}