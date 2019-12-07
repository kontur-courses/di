using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TagsCloudVisualization
{
    internal class TextFromPathReader
    {
        public static IEnumerable<string> FindTextLines(string path)
        {
            if (File.Exists(path))
                return File.ReadLines(path, Encoding.UTF8);
            return null;
        }
    }
}