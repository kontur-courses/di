using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WindowsFormsApp1
{
    public class TxtTextReader : ITextReader
    {
        public IEnumerable<string> Read(string path)
        {
            return File.ReadAllLines(path)
                .Select(x => x.Split())
                .SelectMany(x => x);
        }
    }
}