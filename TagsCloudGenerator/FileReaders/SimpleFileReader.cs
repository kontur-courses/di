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

            var wordToCount = new Dictionary<string, int>();
            
            foreach (var word in words)
            {
                if (wordToCount.ContainsKey(word))
                    wordToCount[word]++;
                else
                    wordToCount.Add(word, 1);
            }
            
            return wordToCount;
        }
    }
}