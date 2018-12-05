using System.Collections.Generic;
using System.IO;
using System.Text;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    public class FileReader : IFileReader
    {
        public string Path { get; set; }

        public FileReader(string path)
            => Path = path;

        public IEnumerable<string> ReadLines()
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