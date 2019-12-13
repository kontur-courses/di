using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace TagsCloudApp.Reader
{
    public class FileTextReader : IFileReader
    {
        public IEnumerable<string> ReadWords(string path)
        {
            if (path is null || !File.Exists(path))
                throw new ArgumentException($"Incorrect file path: {path}");
            var text = File.ReadAllText(path, Encoding.GetEncoding(1251));
            return GetWords(text);
        }

        private IEnumerable<string> GetWords(string text)
        {
            return text
                .Trim()
                .Split(' ', ',', '.', '!', '?', '(', ')', ':', ';', '<', '>', '"', '\n')
                .ToList();
        }
    }
}
