using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud.Readers
{
    public class TextReader : IFileReader
    {
        public string[] ReadFile(string filename)
        {
            return File.ReadAllLines(filename);
        }
    }
}
