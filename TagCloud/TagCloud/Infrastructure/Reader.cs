using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud
{
    public class Reader
    {
        public delegate Reader Factory();

        public Reader(IReader textReader)
        {
            TextReader = textReader;
        }

        public IReader TextReader;

        public string PathToFile { get; set; }

    }
}
