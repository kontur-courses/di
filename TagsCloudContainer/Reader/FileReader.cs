using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using TagsCloudContainer.UI;

namespace TagsCloudContainer.Reader
{
    public class FileReader : ITextReader
    {
        public IEnumerable<string> ReadWords(string path)
        {
            var text = File.ReadAllText(path);
            return text
                .Trim()
                .Split(' ', ',', '.', '!', '?', '(', ')', ':', ';', '<', '>', '"')
                .Where(w => w.Length > 0 && char.IsLetter(w[0]) && char.IsLetter(w[w.Length - 1]));
        }
    }
}
