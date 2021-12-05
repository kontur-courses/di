using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud2
{
    public interface IFileGenerator
    {
        void GenerateFile(string name, IImageFormatter formatter);
    }
}
