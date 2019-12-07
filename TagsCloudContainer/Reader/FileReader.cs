using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace TagsCloudContainer.Reader
{
    public class FileReader : ITextReader
    {
        private readonly string path;

        public FileReader(string path)
        {
            this.path = path;
        }

        private string ReadText()
        {
            var text = File.ReadAllText(path);
            return text;
        }

        public IEnumerable<string> ReadWords()
        {
            var text = ReadText();
            return text
                .Trim()
                .Split(' ', ',', '.', '!', '?', '(', ')', ':', ';', '<', '>', '"')
                .Where(w => w.Length > 0 && char.IsLetter(w[0]) && char.IsLetter(w[w.Length - 1]));
        }
    }
}
