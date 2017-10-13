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

        public IEnumerable<string> ReadLinesToArray()
        {


            return File.ReadLines(filename, Encoding.Default);
        }
    }
}
