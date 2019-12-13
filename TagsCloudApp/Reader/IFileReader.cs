using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudApp.Reader
{
    public interface IFileReader
    {
        IEnumerable<string> ReadWords(string filename);
    }
}
