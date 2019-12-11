using System.Collections.Generic;
using System.IO;
using TagsCloudGenerator.Tools;

namespace TagsCloudGenerator.FileReaders
{
    public class TxtFileReader : IFileReader
    {
        public Dictionary<string, int> ReadWords(string path)
        {
            var words = new List<string>();
            words.AddRange(File.ReadAllLines(path));

            return Helper.GetWordToCount(words);
        }
    }
}