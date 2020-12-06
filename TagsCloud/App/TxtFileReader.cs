using System.Collections.Generic;
using System.IO;

namespace TagsCloud.App
{
    public class TxtFileReader : FileReader
    {
        public override HashSet<string> AvailableFileTypes { get; } = new HashSet<string> {"txt"};

        public override string[] ReadLines(string fileName)
        {
            CheckForExceptions(fileName);
            return File.ReadAllLines(fileName);
        }
    }
}