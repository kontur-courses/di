using System;
using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer.FileReader
{
    public class TxtFileReader : IFileReader
    {
        public string Extension => ".txt";

        public ICollection<string> ReadWords(string path)
        {
            var words = new List<string>();
            if (!File.Exists(path))
                throw new Exception("File is not exist");
            using var reader = new StreamReader(path);
            while (reader.Peek() >= 0)
                words.Add(reader.ReadLine()!);
            return words;
        }
    }
}