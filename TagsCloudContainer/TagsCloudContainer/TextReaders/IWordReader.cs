using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    public interface IWordReader
    {
        IEnumerable<string> Read(string pathToFile);
    }
}
