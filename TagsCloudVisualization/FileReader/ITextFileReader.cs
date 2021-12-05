using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization.FileReader
{
    public interface ITextFileReader
    {
        string[] ReadText(string path, Encoding encoding);
        string[] ReadText(string path);
    }
}
