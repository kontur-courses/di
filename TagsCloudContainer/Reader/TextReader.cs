using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace TagsCloudContainer.Reader
{
    public class TextReader : IFileReader
    {
        public IList<string> ReadWords(string path)
        {
            var directoryInfo = DirectoryMethods.GetProjectDirectoryInfo();
            var fullName = directoryInfo.FullName + $@"\{path}";
            var text = File.ReadAllText(fullName);
            return GetWords(text);
        }

        private IList<string> GetWords(string text)
        {
            return text
                .Trim()
                .Split(' ', ',', '.', '!', '?', '(', ')', ':', ';', '<', '>', '"')
                .Where(w => w.Length > 0 && char.IsLetter(w[0]) && char.IsLetter(w[w.Length - 1]))
                .ToList();
        }
    }
}
