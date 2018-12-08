using System.Collections.Generic;
using System.IO;
using System.Text;
using TagCloud.Interfaces;

namespace TagCloud
{
    public class FileReaderByLines : IFileReader
    {
        public IEnumerable<string> Read(string path)
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