using System.Collections.Generic;
using System.Text;

namespace TagCloud.Words.Writing.ToFile
{
    public interface IFileWriter
    {
        void WriteToFile(string pathToFile, IEnumerable<string> lines, Encoding encoding);
    }
}