using System;
using System.Collections.Generic;
using System.IO;

namespace TagCloud.file_readers
{
    public class TxtReader : IFileReader
    {
        public List<string> GetWords(string filename)
        {
            if (filename is null)
                throw new ArgumentNullException();

            var words = new List<string>();

            using var sr = new StreamReader(filename);
            var line = sr.ReadLine();
            while (line is not null)
            {
                if (!IsWord(line))
                    throw new ArgumentException("Each line in the file must contain only one word");
                words.Add(line);
                line = sr.ReadLine();
            }

            return words;
        }

        private bool IsWord(string word) => word.Length > 0 && !word.Contains(' ');
    }
}