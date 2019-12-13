using System.Collections.Generic;
using System.IO;
using System.Text;
using TagsCloudVisualization.WordSource.Interfaces;

namespace TagsCloudVisualization.WordSource.Readers
{
    class TxtReader:IFileReader<string>
    {
        public IEnumerable<string> ReadLines(string path)
        {
            if (File.Exists(path))
                return File.ReadLines(path, Encoding.UTF8);
            return null;
        }
    }
}
