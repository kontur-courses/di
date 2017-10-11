using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    class TxtFileParser : IFileParser
    {
        private string filename;

        public TxtFileParser(string filename)
        {
            this.filename = filename;
        }

        public string[] ReadLinesToArray()
        {
            return File.ReadAllLines(filename, Encoding.Default);
        }
    }
}
