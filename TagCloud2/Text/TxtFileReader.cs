using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud2
{
    public class TxtFileReader : IFileReader
    {
        public string ReadFile(string path, ITextFormatter formatter)
        {
            return File.ReadAllText(path);
        }
    }
}
