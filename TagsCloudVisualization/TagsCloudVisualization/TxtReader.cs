using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    public class TxtReader : IFileReader
    {
        public string Read(string fileName)
        {
            return File.ReadAllText(fileName, Encoding.UTF8);
        }
    }
}
