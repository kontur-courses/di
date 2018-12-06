using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TagsCloudContainer
{
    public class TextFileReader : IWordsReader
    {
        private readonly string filePath;

        public TextFileReader(string filePath)
        {
            this.filePath = filePath;
        }

        public string[] ReadWords()
        {
            using (var fileStream = File.OpenRead(filePath))
            {
                var array = new byte[fileStream.Length];
                fileStream.Read(array, 0, array.Length);
                var textFromFile = Encoding.Default.GetString(array);
                var separators = new string[] { Environment.NewLine };
                return textFromFile.Split(separators, StringSplitOptions.RemoveEmptyEntries).ToArray();
            }
        }
        public HashSet<string> ReadWordsInHashSet()
        {
            var words = new HashSet<string>();
            foreach (var word in ReadWords())
            {
                words.Add(word);
            }

            return words;
        }
    }

    public interface IWordsReader
    {
        string[] ReadWords();
    }
}
