using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TagsCloudContainer.Readers
{
    class SimpleReader : IReader
    {
        public string[] ReadAllLines(string path)
        {
            var stream = new StreamReader(path);
            return stream.ReadToEnd().Split('\n');
        }
    }
}
