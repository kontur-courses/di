using System.Collections.Generic;
using System.IO;

namespace TagsCloudGenerator.FileReaders
{
    public class SimpleFileReader: IFileReader
    {
        public Dictionary<string, int> ReadWords(string path)
        {
            var words = new List<string>();
            words.AddRange(File.ReadAllLines(path));

            return WordToCountConverter.GetWordToCount(words);
        }
    }
}