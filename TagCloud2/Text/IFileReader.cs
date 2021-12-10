using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud2
{
    public interface IFileReader
    {
        string ReadFile(string path, ITextFormatter formatter);
    }
}
