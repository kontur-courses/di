using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Text;

namespace TagsCloudApp.Reader
{
    public class FileTextReader : IFileReader
    {
        public IEnumerable<string> ReadWords(string path)
        {
            if (path is null || !File.Exists(path))
                throw new ArgumentException($"Incorrect file path: {path}");
            var text = File.ReadAllText(path, Encoding.GetEncoding(1251));
            //Console.WriteLine(text);
            foreach(var m in GetWords(text))
                Console.WriteLine(m);
            return GetWords(text);
        }

        private IEnumerable<string> GetWords(string text)
        {
            return text
                .Trim()
                .Split(' ', ',', '.', '!', '?', '(', ')', ':', ';', '<', '>', '"', '\n')
                .ToList();
                //.Where(w => w.Length > 0 && char.IsLetter(w[0]) && char.IsLetter(w[w.Length - 1]))
                //.ToList();
        }
    }
}
