using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TagsCloud.WordPreprocessing
{
    public class FileReader : IWordGetter
    {
        public readonly FileInfo FileName;

        public readonly Encoding Encoding;
        private readonly Regex _regex = new Regex(@"^\s*$", RegexOptions.Compiled);

        public FileReader(FileInfo fileName, Encoding encoding = null)
        {
            if (!fileName.Exists) throw new FileNotFoundException();
            FileName = fileName;
            Encoding = encoding ?? Encoding.Default;
        }

        public IEnumerable<string> GetWords(params char[] delimiters)
        {
            delimiters = delimiters.ToList().Append(' ').ToArray();
            using (var sr = new StreamReader(FileName.FullName, Encoding))
            {
                return sr.ReadToEnd().Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
                    .Where(w => !_regex.IsMatch(w));
            }
        }
    }
}