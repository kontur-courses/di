using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    public class LineByLineWordReader : IWordReader
    {
        public LineByLineWordReader()
        {

        }

        public IEnumerable<string> Read(string pathToFile)
        {
            return System.IO.File.ReadLines(pathToFile);
        }
    }
}
