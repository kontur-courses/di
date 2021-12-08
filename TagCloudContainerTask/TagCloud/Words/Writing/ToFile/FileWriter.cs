using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TagCloud.Words.Writing.ToFile
{
    public class FileWriter : IFileWriter
    {
        public void WriteToFile(string pathToFile, IEnumerable<string> lines, Encoding encoding)
        {
            File.WriteAllLines(pathToFile, lines, encoding);
        }
    }
}