using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer.DataReader
{
    public class FileReader : IDataReader
    {
        public IEnumerable<string> Read(string filename)
        {
            return File.ReadLines(filename);
        }
    }
}