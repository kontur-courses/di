using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TagsCloudContainer.Words;

namespace TagsCloudContainer
{
    public class TextFileWordsReader : IWordsReader
    {
        private readonly string filePath;

        public TextFileWordsReader(string filePath)
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
            return new HashSet<string>(ReadWords());
        }
    }
}
