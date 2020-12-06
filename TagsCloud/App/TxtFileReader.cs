using System.Collections.Generic;
using System.IO;

namespace TagsCloud.App
{
    public class TxtFileReader : FileReader
    {
        public override HashSet<string> AvailableFileTypes { get; } = new HashSet<string> {"txt"};

        public override IEnumerable<string> ReadLines(string fileName)
        {
            CheckForExceptions(fileName);
            foreach (var line in File.ReadAllLines(fileName))
            {
                foreach (var word in GetWords(line)) 
                    yield return word;
            }
        }
    }
}