using System.Collections.Generic;
using System.IO;
using System.Text;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    public class FileReader : IFileReader
    {
        public IEnumerable<string> ReadLines(string path)
        {
            var result = new List<string>();
            using (var sr = new StreamReader(path, Encoding.Default))
            {
                while (!sr.EndOfStream) result.Add(sr.ReadLine());
            }

            return result;
        }
    }
}